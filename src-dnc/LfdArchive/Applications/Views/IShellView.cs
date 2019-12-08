
using LfdArchive.Domain;
using System.Waf.Applications;

namespace LfdArchive.Applications.Views
{
    internal interface IShellView : IView
    {
        void Show();

        void Close();

        ResourceEntry[] GetSelectedEntries();
    }
}