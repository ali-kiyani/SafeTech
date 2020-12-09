using Abp.Configuration.Startup;
using Abp.Localization.Dictionaries;
using Abp.Localization.Dictionaries.Xml;
using Abp.Reflection.Extensions;

namespace ForeSpark.Localization
{
    public static class ForeSparkLocalizationConfigurer
    {
        public static void Configure(ILocalizationConfiguration localizationConfiguration)
        {
            localizationConfiguration.Sources.Add(
                new DictionaryBasedLocalizationSource(ForeSparkConsts.LocalizationSourceName,
                    new XmlEmbeddedFileLocalizationDictionaryProvider(
                        typeof(ForeSparkLocalizationConfigurer).GetAssembly(),
                        "ForeSpark.Localization.SourceFiles"
                    )
                )
            );
        }
    }
}
