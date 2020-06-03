using NUnit.Framework;
using Microsoft.DotNet.Interactive.Formatting;
using FluentAssertions;
using Microsoft.AspNetCore.Html;
using Microsoft.Data.Analysis;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ML.Data;
using static Microsoft.DotNet.Interactive.Formatting.PocketViewTags;

namespace ApexCode.Interactive.Formatting.UnitTests
{
    public class FormattersUT
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DataFrame_InputDataFrame_ReturnsHtml()
        {
            //Arrange
            PrimitiveDataFrameColumn<int> ints = new PrimitiveDataFrameColumn<int>("Ints", 3); // Makes a column of length 3. Filled with nulls initially
            StringDataFrameColumn strings = new StringDataFrameColumn("Strings", 3); // Makes a column of length 3. Filled with nulls initially
            DataFrame df = new DataFrame(ints, strings); // This will throw if the columns are of different lengths

            //Act
            Formatters.Register<DataFrame>();

            //Assert
            df.ToDisplayString("text/html").Should().Be("<table><thead><th><i>index</i></th><th>Ints</th><th>Strings</th></thead><tbody><tr><td>0</td><td>&lt;null&gt;</td><td>&lt;null&gt;</td></tr><tr><td>1</td><td>&lt;null&gt;</td><td>&lt;null&gt;</td></tr><tr><td>2</td><td>&lt;null&gt;</td><td>&lt;null&gt;</td></tr></tbody></table>");
        }
    }
}