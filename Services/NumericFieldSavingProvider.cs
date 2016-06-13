﻿using Lombiq.FeedAggregator.Models;
using Orchard.ContentManagement;
using Orchard.ContentManagement.MetaData;
using Orchard.Fields.Fields;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Piedone.HelpfulLibraries.Contents;
using System.Globalization;

namespace Lombiq.FeedAggregator.Services
{
    public class NumericFieldSavingProvider : FeedDataSavingProviderBase, IFeedDataSavingProvider
    {
        public string ProviderType { get { return "NumericField"; } }


        public NumericFieldSavingProvider(
            IContentDefinitionManager contentDefinitionManager) :
            base(contentDefinitionManager)
        {
        }


        public bool Save(IFeedDataSavingProviderContext context)
        {
            if (!ProviderIsSuitable(context.Mapping, ProviderType, context.FeedSyncProfilePart.ContentType))
                return false;

            var splitMapping = context.Mapping.ContentItemStorageMapping.Split('.');

            var numericField = context.Content.AsField<NumericField>(splitMapping[0], splitMapping[1]);
            if (numericField == null) return false;

            var decimalValue = default(decimal);
            if (!decimal.TryParse(context.Data, NumberStyles.Number, CultureInfo.InvariantCulture, out decimalValue))
                return false;
            numericField.Value = decimalValue;

            return true;
        }
    }
}