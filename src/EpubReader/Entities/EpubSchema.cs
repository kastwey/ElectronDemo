using Kastwey.EpubReader.Schema.Navigation;
using Kastwey.EpubReader.Schema.Opf;

namespace Kastwey.EpubReader
{
    public class EpubSchema
    {
        public EpubPackage Package { get; set; }
        public EpubNavigation Navigation { get; set; }
        public string ContentDirectoryPath { get; set; }
    }
}
