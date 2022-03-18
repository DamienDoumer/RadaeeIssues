using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RadaeePDFIssue
{
    public interface IPDFLauncher
    {
        Task LaunchPDF();

        Task DownloadPDF();
    }
}
