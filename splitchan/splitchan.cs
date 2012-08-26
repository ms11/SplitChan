using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace splitchan {
    public partial class splitchan : Form {
        public splitchan() {
            InitializeComponent();
            if (!IsRunningOnMono()) {
                if (!File.Exists(".\\audio.png")) {
                    File.Create(".\\test.ogg").Close();
                    var audio = System.Drawing.Icon.ExtractAssociatedIcon(".\\test.ogg");
                    sound_drop.Image = audio.ToBitmap();
                    audio.Dispose();
                    sound_drop.Image.Save(".\\audio.png");
                    File.Delete(".\\test.ogg");
                } else {
                    sound_drop.Image = Bitmap.FromFile(".\\audio.png");
                }
                if (!File.Exists(".\\image.png")) {
                    File.Create(".\\test.png").Close();
                    var image = System.Drawing.Icon.ExtractAssociatedIcon(".\\test.png");
                    img_drop.Image = image.ToBitmap();
                    image.Dispose();
                    img_drop.Image.Save(".\\image.png");
                    File.Delete(".\\test.png");
                } else {
                    img_drop.Image = Bitmap.FromFile(".\\image.png");

                }
            } else {
                sound_drop.Image = new Bitmap(100, 100);
                var gp = Graphics.FromImage(sound_drop.Image);
                gp.DrawString("Sound", Font, Brushes.Black, new PointF(10, 10));
                gp.Flush();
                gp.Dispose();
                img_drop.Image = new Bitmap(100, 100);
                gp = Graphics.FromImage(img_drop.Image);
                gp.DrawString("Image", Font, Brushes.Black, new PointF(10, 10));
                gp.Flush();
                gp.Dispose();
            }
        }
        const int maxSize = 3 * 1024 * 1024 - 200;
        Brush[] colors = new Brush[] { null, Brushes.Yellow, Brushes.Green, Brushes.Black, Brushes.Red, Brushes.Black, Brushes.Cyan, Brushes.Orange, Brushes.White };
        void makeSplitFile(string imagefile, string soundfile, string tag, string outfile) {
            var raw = new FileStream(soundfile, FileMode.Open);
            var basefile = Image.FromFile(imagefile);
            Bitmap bmp = new Bitmap(basefile);
            int id = 1;
            //List<Color> colors = new List<Color>();
            do {
                MemoryStream ms = new MemoryStream();
                //Color c = Color.FromArgb(rnd.Next(0, 255), rnd.Next(0, 255), rnd.Next(0, 255));
                var gr = Graphics.FromImage(bmp);
                /*bmp.SetPixel(0, 0, colors[id]);
                bmp.SetPixel(1, 0, colors[id]);
                bmp.SetPixel(1, 1, colors[id]);
                bmp.SetPixel(0, 1, colors[id]);*/
                var w = (int)bmp.Width / 20;
                var h = (int)bmp.Height / 20;
                gr.FillRectangle(colors[id], new Rectangle(0, 0, w, h));
                bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                var name = Path.ChangeExtension(outfile, id.ToString("D3")) + Path.GetExtension(outfile);
                FileStream fs = new FileStream(name, FileMode.Create);
                ms.Position = 0;
                do {
                    byte[] buffer = new byte[1024];
                    int l = ms.Read(buffer, 0, 1024);
                    if (l < 1) break;
                    fs.Write(buffer, 0, l);
                } while (true);
                //ms.CopyTo(fs);
                uint start = (uint)fs.Position;
                do {
                    byte[] data2 = new byte[1024];
                    int l = raw.Read(data2, 0, 1024);
                    if (l < 1) break;
                    fs.Write(data2, 0, l);
                } while (fs.Length < (maxSize - 1024));
                byte[] taga = Encoding.ASCII.GetBytes(tag + '.' + id.ToString("D3"));
                ushort counter = (ushort)(taga.Length + 9);
                uint end = (uint)fs.Position - 1;  //this 1 will haunt me forever
                fs.WriteByte((byte)taga.Length);
                fs.Write(taga, 0, taga.Length);
                fs.Write(BitConverter.GetBytes(start), 0, 4);
                fs.Write(BitConverter.GetBytes(end), 0, 4);
                byte[] cbytes = BitConverter.GetBytes(counter);
                fs.Write(cbytes, 0, 2);
                byte[] sign = Encoding.ASCII.GetBytes("4SPF");
                fs.Write(sign, 0, 4);
                fs.Close();
                id++;
            } while (raw.Position < raw.Length - 1);
            raw.Close();
        }
        private void sound_drop_DragOver(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Link;
        }

        private void img_drop_DragOver(object sender, DragEventArgs e) {
            e.Effect = DragDropEffects.Link;
        }
        private string ImgDrop;
        private void img_drop_DragDrop(object sender, DragEventArgs e) {
            var filename = ((string[])e.Data.GetData("FileNameW"))[0];
            var ext = Path.GetExtension(filename);
            if (ext != ".png" && ext != ".gif" && ext != ".jpg") {
                MessageBox.Show("Not valid file!");
            } else {
                ImgDrop = filename;
                img_drop.Text = Path.GetFileName(ImgDrop);
            }

        }
        private string SoundDrop;
        private void sound_drop_DragDrop(object sender, DragEventArgs e) {
            var filename = ((string[])e.Data.GetData("FileNameW"))[0];
            var ext = Path.GetExtension(filename);
            if (ext != ".ogg") {
                MessageBox.Show("Not valid file!");
            } else {
                SoundDrop = filename;
                sound_drop.Text = Path.GetFileName(SoundDrop);
            }
        }

        private void tag_name_Leave(object sender, EventArgs e) {
            if (tag_name.Text == "")
                tag_name.Text = "Tag";
        }

        private void tag_name_Enter(object sender, EventArgs e) {
            tag_name.Text = "";
        }

        private void save_btn_Click(object sender, EventArgs e) {
            var sfd = new SaveFileDialog();
            var ext = Path.GetExtension(ImgDrop);
            sfd.AddExtension = true;
            sfd.Filter = "image file(*" + ext + ")|*" + ext;
            sfd.FilterIndex = 0;
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                makeSplitFile(ImgDrop, SoundDrop, tag_name.Text, sfd.FileName);
            }
        }
        public static bool IsRunningOnMono() {
            return Type.GetType("Mono.Runtime") != null;
        }
        public void OpenFile(bool isimage) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = isimage ? "images (*.png,*.gif,*.jpg)|*.png;*.gif;*.jpg" : "sound(*.ogg)|*.ogg";
            ofd.FilterIndex = 0;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (isimage) {
                    ImgDrop = ofd.FileName;
                    img_drop.Text = Path.GetFileName(ImgDrop);
                } else {
                    SoundDrop = ofd.FileName;
                    sound_drop.Text = Path.GetFileName(SoundDrop);
                }
            }
        }

        private void sound_drop_Click(object sender, EventArgs e) {
            OpenFile(false);
        }

        private void img_drop_Click(object sender, EventArgs e) {
            OpenFile(true);
        }

        private void extract_Click(object sender, EventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "images (*.png,*.gif,*.jpg)|*.png;*.gif;*.jpg";
            ofd.FilterIndex = 0;
            ofd.Multiselect = true;
            if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                var sfd = new SaveFileDialog();
                sfd.Filter = "ogg (*.ogg)|*ogg";
                sfd.AddExtension = true;
                sfd.DefaultExt = ".ogg";
                sfd.FilterIndex = 0;
                if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                    var end = new char[] { '4', 'S', 'P', 'F' };
                    var file = sfd.OpenFile();
                    var files = sortfiles(ofd.FileNames);
                    string tagname = "";
                    Dictionary<string, Sound> parts = new Dictionary<string, Sound>();
                    foreach (string f in files) {
                        var fa = File.ReadAllBytes(f);
                        bool match = true;
                        for (int i = 0; i < 4; i++) {
                            if (fa[fa.Length - 4 + i] != end[i]) {
                                MessageBox.Show("not valid file: " + f);
                                match = false;
                            }

                        }
                        if (!match) break;
                        var fstart = fa.Length - 6 - BitConverter.ToUInt16(fa, fa.Length - 6);
                        byte taglen = fa[fstart];
                        var tag = Encoding.ASCII.GetString(fa, fstart + 1, taglen);
                        var start = BitConverter.ToUInt32(fa, fstart + taglen + 1);
                        var endpos = BitConverter.ToUInt32(fa, fstart + taglen + 4 + 1);
                        //file.Write(fa, (int)start, (int)(endpos - start) + 1); //there I have it
                        var a = new Sound();
                        a.data = new byte[(endpos - start) + 1];
                        Array.Copy(fa, start, a.data, 0, a.data.Length);
                        //a.tag = tag;
                        //a.start = (int)start;
                        // a.end = (int)endpos;
                        parts.Add(tag, a);
                        if (tagname == "")
                            tagname = tag.Split('.')[0];
                    }
                    for (int i = 1; i < parts.Count+1; i++) {
                        var data = parts[tagname + "." + i.ToString("D3")].data;
                        file.Write(data, 0, data.Length);
                    }
                    file.Close();
                }
            }
        }

        private string[] sortfiles(string[] p) {
            bool was = false;
            do {
                for (int i = 0; i < p.Length - 1; i++) {
                    if (splitnum(p[i]) < splitnum(p[i + i])) {
                        was = true;
                        var b = p[i];
                        p[i] = p[i + 1];
                        p[i + 1] = b;
                    }
                }
            } while (was);
            return p;
        }
        private int splitnum(string file) {
            var filename = Path.GetFileNameWithoutExtension(file);
            var parts = filename.Split('.');
            return Convert.ToInt32(parts[1]);
        }
        public struct Sound {
            public byte[] data;
        }
    }
}
