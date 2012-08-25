import zipfile,os,datetime
now = datetime.datetime.now().strftime("%Y.%m.%d,%H-%M")
build = zipfile.ZipFile("./splitchan-"+now+".zip","w")
os.chdir("./splitchan/bin/Release/")
build.write("splitchan.exe")
os.chdir("../../../")
build.write("./start.sh")
build.close()
