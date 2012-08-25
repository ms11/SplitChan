import zipfile
build = zipfile.ZipFile(".\\build.zip","w")
build.write(".\\splitchan\\bin\\Release\\splitchan.exe")
build.write(".\\start.sh")
build.close()
