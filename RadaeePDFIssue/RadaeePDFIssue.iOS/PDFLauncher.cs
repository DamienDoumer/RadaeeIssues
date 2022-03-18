using Foundation;
using Newtonsoft.Json;
using PdfKit;
using RadaeeLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(RadaeePDFIssue.iOS.PDFLauncher))]
namespace RadaeePDFIssue.iOS
{
    public class PDFLauncher : IPDFLauncher
    {
        public Task DownloadPDF()
        {
            var downloadDelegate = new SessionDownloadDelegate();
            // HEre is the file URL on Google cloud. You can get its download link here too if it expires. "https://drive.google.com/file/d/1ULrU9rRLOD_zlpxQnsECkReSKuJuX4II/view?usp=sharing"
            downloadDelegate.StartStreaming("https://drive.google.com/u/2/uc?id=1ULrU9rRLOD_zlpxQnsECkReSKuJuX4II&export=download",
                $"{FileSystem.AppDataDirectory}/test.pdf");

            return Task.CompletedTask;
        } 

        public async Task LaunchPDF()
        {
            var pdfURL = $"{FileSystem.AppDataDirectory}/test.pdf";


            //Note: Using PDFKit to get the number of pages
            //PDFKit recognizes the file's autenticity. and can open it well. 
            var numberOfPages = GetPageCount(pdfURL, "");

            var everyFile = Directory.GetFileSystemEntries(FileSystem.AppDataDirectory, "*", SearchOption.AllDirectories);
            var str = JsonConvert.SerializeObject(everyFile);

            //Plugin init
            var plugin = RadaeePDFPlugin.PluginInit;

            //Activate license
            plugin.ActivateLicenseWithBundleId("com.radaee.pdf.PDFViewer", "Radaee", "radaee_com@yahoo.cn", "89WG9I-HCL62K-H3CRUZ-WAJQ9H-FADG6Z-XEBCAO");

            //Thumbnail settings
            plugin.SetThumbnailBGColor(Convert.ToInt32("0x88000000", 16)); //AARRGGBB
            plugin.SetThumbHeight(100);

            RDVGlobal.SharedInstance.G_render_mode = 3; // Set render mode

            //Open from assets
            UIViewController controller =
                (UIKit.UIViewController)plugin.Show(pdfURL, "");
            //controller.ModalPresentationStyle = UIModalPresentationStyle.CurrentContext;
            var navController = new UINavigationController(controller);
            navController.NavigationBar.BarTintColor = UIColor.Black;
            navController.NavigationBar.TintColor = UIColor.Orange;
            navController.HidesBottomBarWhenPushed = true;

            //Set Callback for RadaeeDelegate
            var selector = new RadaeePDFDelegate(plugin);
            plugin.SetDelegate(selector);
            var topViewController = Utilities.GetTopViewController();

            //NavigationController.
            //topViewController.NavigationController.NavigationBar.BarTintColor = UIColor.Black;
            //topViewController.NavigationController.NavigationBar.TintColor = UIColor.Orange;

            if (controller != null)
            {
                await topViewController.PresentViewControllerAsync(navController, true);
                //Show reader
                //topViewController.NavigationController.PushViewController(controller, true);
            }
        }

        int GetPageCount(string path, string password)
        {
            //Use PDF Kit to get the document.
            var document = new PdfDocument(NSUrl.CreateFileUrl(path, null));
            var unlocked = document.Unlock(password);
            if (!unlocked)
                throw new Exception("Error occured while unlocking the PDF document.");

            var pageCount = document.PageCount;
            document.Dispose();
            document = null;
            return (int)pageCount;
        }
    }
}