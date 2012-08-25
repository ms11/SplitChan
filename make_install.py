import zipfile,os
build = zipfile.ZipFile("./build.zip","w")
os.chdir("./splitchan/bin/Release/")
build.write("splitchan.exe")
os.chdir("../../../")
build.write("./start.sh")
build.close()
