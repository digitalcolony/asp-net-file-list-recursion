# Using Recursion To Return a List of all Files on a Web Site (`ASP.NET`)

When I decided to build a sitemap for my coffee site [INeedCoffee](https://ineedcoffee.com), I was able to pull a list of URLs from the database. One simple query provided me with all the URLS on the web site. But what if you don’t have a database table holding all the URLs for your site? Maybe the only way to get a full list of URLs is to go through each folder on the site and make a list. Sounds like a job for code.

## Recursion To the Rescue

The job we want the code to perform is to start in the root folder and build a list of files with the .ASPX extension. In this example .ASPX files are the content files. You might add .HTML or .ASP files depending on how you setup your web site. Once you have a list of files for the root folder, the code is to go inside each subfolder, add to the list of files and repeat the process until it’s exhausted the entire tree structure of your web site.

## The Code

When executed, **scan-website-for-files.aspx.cs** will build a list of every .ASPX file on the web server. Note that I add an underscore to the beginning of files that I don’t wish to include on the list of indexed URLs.

```cs
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
```

```cs
string dirName = HttpContext.Current.Server.MapPath("~/");
DirectoryInfo rootDirectory = new DirectoryInfo(dirName);
ScanWebsiteForFiles(rootDirectory);
```

_created: June 13, 2007_