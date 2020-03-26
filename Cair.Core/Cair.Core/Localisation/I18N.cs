using Cair.Core.PlatformDependent;
using System.Globalization;
using System.IO;

namespace Cair.Core.Localisation
{
    public static class I18N
    {
        public static string Domain = "ExPay";

        public static string _(string value)
        {
            return catalog.GetString(value);
        }

        private static CatalogBase catalog = new CatalogLoader(new JsonCatalog());
    }

    public class CatalogLoader
    {
        private CatalogBase _catalog;

        public CatalogLoader(CatalogBase catalog)
        {
            this._catalog = catalog;
        }

        public static implicit operator CatalogBase(CatalogLoader loader)
        {
            var currentCulture = CultureInfo.CurrentCulture;
            //ToDo: replace I18N File loading with assembly resource loading
            var path = Path.Combine(Allocator.New<IDefaultPath>().ToString(), "Locales", currentCulture.Name + loader._catalog.FileExtension);

            if (File.Exists(path))
            {
                loader._catalog.Load(path);
            }
            else
            {
                loader._catalog.Load(path.Replace(currentCulture.Name, "en"));
            }

            return loader._catalog;
        }
    }
}