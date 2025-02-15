﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Waf.Foundation;
using System.Waf.Applications;
using System.ComponentModel;

namespace Test.Waf.Applications
{
    [TestClass]
    public class Pop3SettingsViewModelTest
    {
        [TestMethod]
        public void ViewModelTest()
        {
            var view = new MockPop3SettingsView();
            var pop3Settings = new Pop3Settings();
            var viewModel = new Pop3SettingsViewModel(view, pop3Settings);

            Assert.AreEqual(view, viewModel.View);
            Assert.AreEqual(viewModel, view.DataContext);

            Assert.IsFalse(viewModel.Pop3SettingsServerPathChanged);
            pop3Settings.ServerPath = "pop.mail.com";
            Assert.IsTrue(viewModel.Pop3SettingsServerPathChanged);
        }


        private class Pop3SettingsViewModel : ViewModel<IPop3SettingsView>
        {
            public Pop3SettingsViewModel(IPop3SettingsView view, Pop3Settings pop3Settings) : base(view)
            {
                PropertyChangedEventManager.AddHandler(pop3Settings, Pop3SettingsPropertyChanged, "");
            }

            public bool Pop3SettingsServerPathChanged { get; set; }

            private void Pop3SettingsPropertyChanged(object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == nameof(Pop3Settings.ServerPath))
                {
                    Pop3SettingsServerPathChanged = true;
                }
            }
        }

        private interface IPop3SettingsView : IView { }

        private class MockPop3SettingsView : IPop3SettingsView
        {
            public object DataContext { get; set; }
        }

        private class Pop3Settings : Model
        {
            private string serverPath;

            public string ServerPath
            {
                get => serverPath;
                set => SetProperty(ref serverPath, value);
            }
        }
    }
}
