public ArrayList urlList;

public void ScanWebsiteForFiles(DirectoryInfo directory)
{
   // look for .ASPX files in current folder
   foreach (FileInfo file in directory.GetFiles("*.aspx"))
   {
        // hide files that start with an underscore
       if (!file.Name.StartsWith("_"))
       {
           string thisURL = file.FullName.ToString();
           urlList.Add(thisURL.ToLower());
       }
   }

   DirectoryInfo[] subDirectories = directory.GetDirectories();
   foreach (DirectoryInfo subDirectory in subDirectories)
   {
       ScanWebsiteForFiles(subDirectory);
   }
}

string dirName = HttpContext.Current.Server.MapPath("~/");
DirectoryInfo rootDirectory = new DirectoryInfo(dirName);
ScanWebsiteForFiles(rootDirectory);
