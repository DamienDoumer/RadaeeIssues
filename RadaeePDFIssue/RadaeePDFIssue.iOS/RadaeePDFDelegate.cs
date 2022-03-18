using CoreGraphics;
using Foundation;
using RadaeeLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UIKit;

namespace RadaeePDFIssue.iOS
{
    public class RadaeePDFDelegate : RadaeePDFPluginDelegate
    {
        RadaeePDFPlugin plugin;

        public RadaeePDFDelegate(RadaeePDFPlugin plugin)
        {
            this.plugin = plugin;
        }

        public override void DidChangePage(int page)
        {

        }

        public override async void WillCloseReader()
        {

        }

        public override void DidCloseReader()
        {
            //TODO: Handle this later.
        }

        public override void DidDoubleTapOnPage(int page, CGPoint point)
        {
            //TODO: Handle this later.
        }

        public override void DidLongPressOnPage(int page, CGPoint point)
        {
            //TODO: Handle this later.
        }

        public override void DidSearchTerm(string term, bool found)
        {
            //TODO: Handle this later.
        }

        public override void DidShowReader()
        {
            //TODO: Handle this later.
        }

        public override void DidTapOnAnnotationOfType(int type, int page, CGPoint point)
        {
            //TODO: Handle this later.
        }

        public override void DidTapOnPage(int page, CGPoint point)
        {
            //TODO: Handle this later.
        }

        public override void OnAnnotExported(string path)
        {
            //TODO: Handle this later.
        }

        public override void WillShowReader()
        {
            //TODO: Handle this later.
        }
    }
}