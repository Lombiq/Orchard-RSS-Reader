﻿using Lombiq.RssReader.Constants;
using Lombiq.RssReader.Models.NonPersistent;
using Orchard.ContentManagement;
using Orchard.ContentManagement.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Piedone.HelpfulLibraries.Contents;
using Orchard.Fields.Fields;

namespace Lombiq.RssReader.Models
{
    public class RssSyncProfilePart : ContentPart
    {
        public string ContentType
        {
            get { return this.Retrieve(x => x.ContentType); }
            set { this.Store(x => x.ContentType, value); }
        }

        public string MappingsSerialized
        {
            get { return this.Retrieve(x => x.MappingsSerialized); }
            set { this.Store(x => x.MappingsSerialized, value); }
        }

        public bool SuccesfulInit
        {
            get { return this.Retrieve(x => x.SuccesfulInit); }
            set { this.Store(x => x.SuccesfulInit, value); }
        }

        private readonly LazyField<List<Mapping>> _mappings = new LazyField<List<Mapping>>();
        internal LazyField<List<Mapping>> MappingsField { get { return _mappings; } }
        public List<Mapping> Mappings { get { return _mappings.Value; } }

        public decimal? MinutesBetweenSyncs
        {
            get
            {
                return this.AsField<NumericField>(
                    typeof(RssSyncProfilePart).Name,
                    FieldNames.MinutesBetweenSyncs).Value;
            }
        }
    }
}