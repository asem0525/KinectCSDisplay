using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Microsoft.Samples.Kinect.ControlsBasics.Helper_Classes
{
    public class BBCNews
    {

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]        
        [System.Xml.Serialization.XmlRoot(Namespace = "", IsNullable = false)]
        public partial class rss
        {

            private rssChannel channelField;

            private decimal versionField;

            /// <remarks/>
            public rssChannel channel
            {
                get
                {
                    return this.channelField;
                }
                set
                {
                    this.channelField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            [XmlIgnore]
            public decimal version
            {
                get
                {
                    return this.versionField;
                }
                set
                {
                    this.versionField = value;
                }
            }
            
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class rssChannel
        {

            private string titleField;

            private string descriptionField;

            private string linkField;

            private rssChannelImage imageField;

            private string generatorField;

            private string lastBuildDateField;

            private string copyrightField;

            private string languageField;

            private byte ttlField;

            private rssChannelItem[] itemField;

            /// <remarks/>
            public string title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }

            /// <remarks/>
            public string description
            {
                get
                {
                    return this.descriptionField;
                }
                set
                {
                    this.descriptionField = value;
                }
            }

            /// <remarks/>
            public string link
            {
                get
                {
                    return this.linkField;
                }
                set
                {
                    this.linkField = value;
                }
            }

            /// <remarks/>
            public rssChannelImage image
            {
                get
                {
                    return this.imageField;
                }
                set
                {
                    this.imageField = value;
                }
            }

            /// <remarks/>
            public string generator
            {
                get
                {
                    return this.generatorField;
                }
                set
                {
                    this.generatorField = value;
                }
            }

            /// <remarks/>
            public string lastBuildDate
            {
                get
                {
                    return this.lastBuildDateField;
                }
                set
                {
                    this.lastBuildDateField = value;
                }
            }

            /// <remarks/>
            public string copyright
            {
                get
                {
                    return this.copyrightField;
                }
                set
                {
                    this.copyrightField = value;
                }
            }

            /// <remarks/>
            public string language
            {
                get
                {
                    return this.languageField;
                }
                set
                {
                    this.languageField = value;
                }
            }

            /// <remarks/>
            public byte ttl
            {
                get
                {
                    return this.ttlField;
                }
                set
                {
                    this.ttlField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute("item")]
            public rssChannelItem[] item
            {
                get
                {
                    return this.itemField;
                }
                set
                {
                    this.itemField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class rssChannelImage
        {

            private string urlField;

            private string titleField;

            private string linkField;

            /// <remarks/>
            public string url
            {
                get
                {
                    return this.urlField;
                }
                set
                {
                    this.urlField = value;
                }
            }

            /// <remarks/>
            public string title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }

            /// <remarks/>
            public string link
            {
                get
                {
                    return this.linkField;
                }
                set
                {
                    this.linkField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class rssChannelItem
        {

            private string titleField;

            private string descriptionField;

            private string linkField;

            private rssChannelItemGuid guidField;

            private string pubDateField;

            private thumbnail thumbnailField;

            /// <remarks/>
            public string title
            {
                get
                {
                    return this.titleField;
                }
                set
                {
                    this.titleField = value;
                }
            }

            /// <remarks/>
            public string description
            {
                get
                {
                    return this.descriptionField;
                }
                set
                {
                    this.descriptionField = value;
                }
            }

            /// <remarks/>
            public string link
            {
                get
                {
                    return this.linkField;
                }
                set
                {
                    this.linkField = value;
                }
            }

            /// <remarks/>
            public rssChannelItemGuid guid
            {
                get
                {
                    return this.guidField;
                }
                set
                {
                    this.guidField = value;
                }
            }

            /// <remarks/>
            public string pubDate
            {
                get
                {
                    return this.pubDateField;
                }
                set
                {
                    this.pubDateField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlElementAttribute(Namespace = "http://search.yahoo.com/mrss/")]
            public thumbnail thumbnail
            {
                get
                {
                    return this.thumbnailField;
                }
                set
                {
                    this.thumbnailField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
        public partial class rssChannelItemGuid
        {

            private bool isPermaLinkField;

            private string valueField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public bool isPermaLink
            {
                get
                {
                    return this.isPermaLinkField;
                }
                set
                {
                    this.isPermaLinkField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlTextAttribute()]
            public string Value
            {
                get
                {
                    return this.valueField;
                }
                set
                {
                    this.valueField = value;
                }
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://search.yahoo.com/mrss/")]
        [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://search.yahoo.com/mrss/", IsNullable = false)]
        public partial class thumbnail
        {

            private ushort widthField;

            private ushort heightField;

            private string urlField;

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
#pragma warning disable CS3003 // Type is not CLS-compliant
            public ushort width
#pragma warning restore CS3003 // Type is not CLS-compliant
            {
                get
                {
                    return this.widthField;
                }
                set
                {
                    this.widthField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
#pragma warning disable CS3003 // Type is not CLS-compliant
            public ushort height
#pragma warning restore CS3003 // Type is not CLS-compliant
            {
                get
                {
                    return this.heightField;
                }
                set
                {
                    this.heightField = value;
                }
            }

            /// <remarks/>
            [System.Xml.Serialization.XmlAttributeAttribute()]
            public string url
            {
                get
                {
                    return this.urlField;
                }
                set
                {
                    this.urlField = value;
                }
            }
        }


    }
}
