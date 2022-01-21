﻿using System.IO;
using System.Reflection;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using AddinManager.Command;
using AddinManager.Model;
using Autodesk.Revit.UI;

namespace AddinManager
{
    public class App : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            CreateRibbonPanel(application);
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Cancelled;
        }
        private static void CreateRibbonPanel(UIControlledApplication application)
        {
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("External Tools");
            PulldownButtonData pulldownButtonData = new PulldownButtonData("Options", "Add-in Manager");
            PulldownButton PulldownButton = (PulldownButton)ribbonPanel.AddItem(pulldownButtonData);
            PulldownButton.Image = SetImage(Resource.dev1);
            PulldownButton.LargeImage = SetLargeImage(Resource.dev1);
            AddPushButton(PulldownButton, typeof(AddInManagerManual), "Add-In Manager(Manual Mode)");
            AddPushButton(PulldownButton, typeof(AddInManagerFaceless), "Add-In Manager(Manual Mode,Faceless)");
            AddPushButton(PulldownButton, typeof(AddInManagerReadOnly), "Add-In Manager(Read Only Mode)");
        }

        private static PushButton AddPushButton(PulldownButton pullDownButton, Type command, string buttonText)
        {
            PushButtonData buttonData = new PushButtonData(command.FullName, buttonText, Assembly.GetAssembly(command).Location, command.FullName);
            return pullDownButton.AddPushButton(buttonData);
        }

        private static ImageSource SetImage(Bitmap bitmap)
        {
           return BitmapSourceConverter.ConvertFromImage(bitmap).Resize(16);
        }
        private static ImageSource SetLargeImage(Bitmap bitmap)
        {
            return BitmapSourceConverter.ConvertFromImage(bitmap).Resize(32);
        }
    }
}