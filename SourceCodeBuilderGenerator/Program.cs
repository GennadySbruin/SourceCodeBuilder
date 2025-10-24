// See https://aka.ms/new-console-template for more information
using SourceCodeBuilderGenerator.Html;

var specificationModel = typeof(Step1Specification);
var reflectionModel = new Step2ReflectionModel(specificationModel);
Step3GenerateCode.GenerateCsCode(reflectionModel,
    @"C:\Users\Геннадий\Source\Repos\SourceCodeBuilder\SourceCodeBuilder.Html\Html.cs",
    "SourceCodeBuilder.Html.Expressions");
//Step3GenerateCode.GenerateCsCode(reflectionModel,
//    @"C:\Users\Admin\Source\Repos\GennadySbruin\SourceCodeBuilder\SourceCodeBuilder.Html\Html.cs",
//    "SourceCodeBuilder.Html.Expressions");


