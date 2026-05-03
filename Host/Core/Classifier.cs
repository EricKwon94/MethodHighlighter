using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace Host.Core;

internal class Classifier : IClassifier
{
    private readonly IClassificationType _classificationType;

    private ITextSnapshot? _lastSnapshot;
    private SyntaxNode? _lastRoot;

    public event EventHandler<ClassificationChangedEventArgs>? ClassificationChanged;

    internal Classifier(IClassificationType classificationType)
    {
        _classificationType = classificationType;
    }

    public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
    {
        var result = new List<ClassificationSpan>();
        ITextSnapshot snapshot = span.Snapshot;
        SyntaxNode? root = GetRoot(snapshot);

        if (root == null)
            return result;

        var methodDeclarations = root.DescendantNodes().OfType<MethodDeclarationSyntax>();

        foreach (var method in methodDeclarations)
        {
            SyntaxToken identifier = method.Identifier;
            var identifierSpan = new Span(identifier.SpanStart, identifier.Span.Length);

            if (identifierSpan.IntersectsWith(span.Span))
            {
                var snapshotSpan = new SnapshotSpan(snapshot, identifierSpan);
                result.Add(new ClassificationSpan(snapshotSpan, _classificationType));
            }
        }
        return result;
    }

    private SyntaxNode? GetRoot(ITextSnapshot snapshot)
    {
        if (_lastSnapshot == snapshot)
            return _lastRoot;

        _lastRoot = CSharpSyntaxTree.ParseText(snapshot.GetText()).GetRoot();
        _lastSnapshot = snapshot;
        return _lastRoot;
    }
}
