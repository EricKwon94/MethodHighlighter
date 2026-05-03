using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace Host.Core;

internal static class ClassificationDefinitions
{
    [Export(typeof(ClassificationTypeDefinition))]
    [Name(Const.ClassificationTypeNames)]
    internal static ClassificationTypeDefinition? MethodHighlightType { get; set; }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = Const.ClassificationTypeNames)]
    [Name(Const.ClassificationTypeNames)]
    [UserVisible(true)]
    [Order(After = Priority.High)]
    internal sealed class MethodHighlightFormat : ClassificationFormatDefinition
    {
        public MethodHighlightFormat()
        {
            DisplayName = Const.DisplayName;
            ForegroundColor = System.Windows.Media.Colors.Red;
        }
    }
}
