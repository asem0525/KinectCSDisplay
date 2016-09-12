using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Samples.Kinect.ControlsBasics
{
    class VisibleCSEvent
    {
        public string csEventTitle { get; set; }
        public string csEventTime { get; set; }
        public string csEventLocation { get; set; }
    }
    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class nodes
    {

        private nodesNode[] nodeField;

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("node")]
        public nodesNode[] node
        {
            get
            {
                return this.nodeField;
            }
            set
            {
                this.nodeField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nodesNode
    {

        private string titleField;

        private ushort guidField;

        private string postdateField;

        private string startdateField;

        private string locationField;

        private string tagField;

        private nodesNodeLink linkField;

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

#pragma warning disable CS3003 // Type is not CLS-compliant
                              /// <remarks/>
        public ushort guid
#pragma warning restore CS3003 // Type is not CLS-compliant
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
        public string postdate
        {
            get
            {
                return this.postdateField;
            }
            set
            {
                this.postdateField = value;
            }
        }

        /// <remarks/>
        public string startdate
        {
            get
            {
                return this.startdateField;
            }
            set
            {
                this.startdateField = value;
            }
        }

        /// <remarks/>
        public string location
        {
            get
            {
                return this.locationField;
            }
            set
            {
                this.locationField = value;
            }
        }

        /// <remarks/>
        public string tag
        {
            get
            {
                return this.tagField;
            }
            set
            {
                this.tagField = value;
            }
        }

        /// <remarks/>
        public nodesNodeLink link
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
    public partial class nodesNodeLink
    {

        private nodesNodeLinkA aField;

        /// <remarks/>
        public nodesNodeLinkA a
        {
            get
            {
                return this.aField;
            }
            set
            {
                this.aField = value;
            }
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
    public partial class nodesNodeLinkA
    {

        private string hrefField;

        private string valueField;

        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string href
        {
            get
            {
                return this.hrefField;
            }
            set
            {
                this.hrefField = value;
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


}
