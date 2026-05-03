using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Host.Core;

[Export(typeof(IClassifierProvider))]
[ContentType("CSharp")]
internal class ClassifierProvider : IClassifierProvider
{
    [Import]
    internal IClassificationTypeRegistryService? Registry { get; set; }

    public IClassifier GetClassifier(ITextBuffer buffer)
    {
        IClassificationType type = Registry!.GetClassificationType(Const.ClassificationTypeNames);
        Classifier classifier = buffer.Properties.GetOrCreateSingletonProperty(() => new Classifier(type));
        return classifier;
    }
}