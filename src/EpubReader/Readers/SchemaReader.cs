using System.IO.Compression;
using System.Threading.Tasks;
using Kastwey.EpubReader.Schema.Navigation;
using Kastwey.EpubReader.Schema.Opf;
using Kastwey.EpubReader.Utils;

namespace Kastwey.EpubReader.Readers
{
    internal static class SchemaReader
    {
        public static async Task<EpubSchema> ReadSchemaAsync(ZipArchive epubArchive)
        {
            EpubSchema result = new EpubSchema();
            string rootFilePath = await RootFilePathReader.GetRootFilePathAsync(epubArchive).ConfigureAwait(false);
            string contentDirectoryPath = ZipPathUtils.GetDirectoryPath(rootFilePath);
            result.ContentDirectoryPath = contentDirectoryPath;
            EpubPackage package = await PackageReader.ReadPackageAsync(epubArchive, rootFilePath).ConfigureAwait(false);
            result.Package = package;
            EpubNavigation navigation = await NavigationReader.ReadNavigationAsync(epubArchive, contentDirectoryPath, package).ConfigureAwait(false);
            result.Navigation = navigation;
            return result;
        }
    }
}
