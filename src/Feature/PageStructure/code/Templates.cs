using Sitecore.Data;

namespace Noesis.Feature.PageStructure
{
    public struct Templates
    {
        public struct PageSection
        {
            public static readonly ID Id = new ID("{1325DD59-0866-4F34-8B1D-047B236C5C02}");

            public struct Fields
            {
                public static readonly ID BackgroundColor = new ID("{F4107168-5C09-4363-B163-0FF88B7FC6F8}");
                public static readonly ID BackgroundImage = new ID("{85B4D88B-7D00-434F-998B-3834BB1AEBAF}");
                public static readonly ID InternalVideo = new ID("{19FBF70C-6E78-4E71-9974-347C668508EB}");
                public static readonly ID Autoplay = new ID("{A8B57418-76D9-4B2E-B769-CBFA03030A41}");
                public static readonly ID GoalId = new ID("{23B6D12A-AE08-4F95-9A7D-BE3A79D8BE9D}");
            }
        }
    }
}