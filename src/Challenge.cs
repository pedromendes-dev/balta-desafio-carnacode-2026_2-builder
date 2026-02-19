#:property PublishAot=false

// DESAFIO: Gerador de Relatórios Complexos
// PROBLEMA: Sistema precisa gerar diferentes tipos de relatórios (PDF, Excel, HTML)
// com múltiplas configurações opcionais (cabeçalho, rodapé, gráficos, tabelas, filtros)
// O código atual usa construtores enormes ou muitos setters, tornando difícil criar relatórios

using System;
using System.Collections.Generic;

namespace DesignPatternChallenge
{
    public class SalesReport
    {
        public string Title { get; private set; }
        public string Format { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }
        public bool IncludeHeader { get; private set; }
        public bool IncludeFooter { get; private set; }
        public string HeaderText { get; private set; }
        public string FooterText { get; private set; }
        public bool IncludeCharts { get; private set; }
        public string ChartType { get; private set; }
        public bool IncludeSummary { get; private set; }
        public List<string> Columns { get; private set; }
        public List<string> Filters { get; private set; }
        public string SortBy { get; private set; }
        public string GroupBy { get; private set; }
        public bool IncludeTotals { get; private set; }
        public string Orientation { get; private set; }
        public string PageSize { get; private set; }
        public bool IncludePageNumbers { get; private set; }
        public string CompanyLogo { get; private set; }
        public string WaterMark { get; private set; }

        internal SalesReport()
        {
            Title = string.Empty;
            Format = string.Empty;
            HeaderText = string.Empty;
            FooterText = string.Empty;
            ChartType = string.Empty;
            SortBy = string.Empty;
            GroupBy = string.Empty;
            Orientation = "Portrait";
            PageSize = "A4";
            CompanyLogo = string.Empty;
            WaterMark = string.Empty;
            Columns = new List<string>();
            Filters = new List<string>();
        }

        internal void SetTitle(string title) => Title = title;
        internal void SetFormat(string format) => Format = format;
        internal void SetPeriod(DateTime startDate, DateTime endDate)
        {
            StartDate = startDate;
            EndDate = endDate;
        }
        internal void SetHeader(string headerText)
        {
            IncludeHeader = true;
            HeaderText = headerText;
        }
        internal void SetFooter(string footerText)
        {
            IncludeFooter = true;
            FooterText = footerText;
        }
        internal void SetChart(string chartType)
        {
            IncludeCharts = true;
            ChartType = chartType;
        }
        internal void SetSummary(bool includeSummary) => IncludeSummary = includeSummary;
        internal void AddColumn(string column) => Columns.Add(column);
        internal void AddFilter(string filter) => Filters.Add(filter);
        internal void SetSortBy(string sortBy) => SortBy = sortBy;
        internal void SetGroupBy(string groupBy) => GroupBy = groupBy;
        internal void SetTotals(bool includeTotals) => IncludeTotals = includeTotals;
        internal void SetOrientation(string orientation) => Orientation = orientation;
        internal void SetPageSize(string pageSize) => PageSize = pageSize;
        internal void SetPageNumbers(bool includePageNumbers) => IncludePageNumbers = includePageNumbers;
        internal void SetCompanyLogo(string companyLogo) => CompanyLogo = companyLogo;
        internal void SetWaterMark(string waterMark) => WaterMark = waterMark;

        public void Generate()
        {
            Console.WriteLine($"\n=== Gerando Relatório: {Title} ===");
            Console.WriteLine($"Formato: {Format}");
            Console.WriteLine($"Período: {StartDate:dd/MM/yyyy} a {EndDate:dd/MM/yyyy}");
            
            if (IncludeHeader)
                Console.WriteLine($"Cabeçalho: {HeaderText}");
            
            if (IncludeCharts)
                Console.WriteLine($"Gráfico: {ChartType}");
            
            Console.WriteLine($"Colunas: {string.Join(", ", Columns)}");
            
            if (Filters.Count > 0)
                Console.WriteLine($"Filtros: {string.Join(", ", Filters)}");
            
            if (!string.IsNullOrEmpty(GroupBy))
                Console.WriteLine($"Agrupado por: {GroupBy}");
            
            if (IncludeFooter)
                Console.WriteLine($"Rodapé: {FooterText}");
            
            Console.WriteLine("Relatório gerado com sucesso!");
        }
    }

    public interface IReportBuilder
    {
        IReportBuilder WithTitle(string title);
        IReportBuilder InFormat(string format);
        IReportBuilder FromPeriod(DateTime startDate, DateTime endDate);
        IReportBuilder WithHeader(string headerText);
        IReportBuilder WithFooter(string footerText);
        IReportBuilder WithChart(string chartType);
        IReportBuilder WithSummary();
        IReportBuilder AddColumn(string column);
        IReportBuilder AddFilter(string filter);
        IReportBuilder SortBy(string sortBy);
        IReportBuilder GroupBy(string groupBy);
        IReportBuilder WithTotals();
        IReportBuilder InOrientation(string orientation);
        IReportBuilder InPageSize(string pageSize);
        IReportBuilder WithPageNumbers();
        IReportBuilder WithCompanyLogo(string companyLogo);
        IReportBuilder WithWaterMark(string waterMark);
        SalesReport Build();
    }

    public class SalesReportBuilder : IReportBuilder
    {
        private readonly SalesReport _report = new();

        public IReportBuilder WithTitle(string title)
        {
            _report.SetTitle(title);
            return this;
        }

        public IReportBuilder InFormat(string format)
        {
            _report.SetFormat(format);
            return this;
        }

        public IReportBuilder FromPeriod(DateTime startDate, DateTime endDate)
        {
            _report.SetPeriod(startDate, endDate);
            return this;
        }

        public IReportBuilder WithHeader(string headerText)
        {
            _report.SetHeader(headerText);
            return this;
        }

        public IReportBuilder WithFooter(string footerText)
        {
            _report.SetFooter(footerText);
            return this;
        }

        public IReportBuilder WithChart(string chartType)
        {
            _report.SetChart(chartType);
            return this;
        }

        public IReportBuilder WithSummary()
        {
            _report.SetSummary(true);
            return this;
        }

        public IReportBuilder AddColumn(string column)
        {
            _report.AddColumn(column);
            return this;
        }

        public IReportBuilder AddFilter(string filter)
        {
            _report.AddFilter(filter);
            return this;
        }

        public IReportBuilder SortBy(string sortBy)
        {
            _report.SetSortBy(sortBy);
            return this;
        }

        public IReportBuilder GroupBy(string groupBy)
        {
            _report.SetGroupBy(groupBy);
            return this;
        }

        public IReportBuilder WithTotals()
        {
            _report.SetTotals(true);
            return this;
        }

        public IReportBuilder InOrientation(string orientation)
        {
            _report.SetOrientation(orientation);
            return this;
        }

        public IReportBuilder InPageSize(string pageSize)
        {
            _report.SetPageSize(pageSize);
            return this;
        }

        public IReportBuilder WithPageNumbers()
        {
            _report.SetPageNumbers(true);
            return this;
        }

        public IReportBuilder WithCompanyLogo(string companyLogo)
        {
            _report.SetCompanyLogo(companyLogo);
            return this;
        }

        public IReportBuilder WithWaterMark(string waterMark)
        {
            _report.SetWaterMark(waterMark);
            return this;
        }

        public SalesReport Build()
        {
            if (string.IsNullOrWhiteSpace(_report.Title))
                throw new InvalidOperationException("Título é obrigatório");

            if (string.IsNullOrWhiteSpace(_report.Format))
                throw new InvalidOperationException("Formato é obrigatório");

            if (_report.StartDate == default || _report.EndDate == default)
                throw new InvalidOperationException("Período é obrigatório");

            if (_report.Columns.Count == 0)
                throw new InvalidOperationException("Pelo menos uma coluna é obrigatória");

            return _report;
        }
    }

    public class ReportDirector
    {
        public SalesReport CreateMonthlyPdf(IReportBuilder builder)
        {
            return builder
                .WithTitle("Vendas Mensais")
                .InFormat("PDF")
                .FromPeriod(new DateTime(2024, 1, 1), new DateTime(2024, 1, 31))
                .WithHeader("Relatório de Vendas")
                .WithFooter("Confidencial")
                .AddColumn("Produto")
                .AddColumn("Quantidade")
                .AddColumn("Valor")
                .WithChart("Bar")
                .AddFilter("Status=Ativo")
                .SortBy("Valor")
                .GroupBy("Categoria")
                .WithTotals()
                .InOrientation("Portrait")
                .InPageSize("A4")
                .WithPageNumbers()
                .WithWaterMark("Confidencial")
                .Build();
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=== Sistema de Relatórios ===");

            var director = new ReportDirector();
            var monthlyPdf = director.CreateMonthlyPdf(new SalesReportBuilder());
            monthlyPdf.Generate();

            var quarterlyExcel = new SalesReportBuilder()
                .WithTitle("Relatório Trimestral")
                .InFormat("Excel")
                .FromPeriod(new DateTime(2024, 1, 1), new DateTime(2024, 3, 31))
                .WithHeader("Consolidado Trimestral")
                .AddColumn("Vendedor")
                .AddColumn("Região")
                .AddColumn("Total")
                .WithChart("Line")
                .GroupBy("Região")
                .WithTotals()
                .Build();

            quarterlyExcel.Generate();

            var yearlyHtml = new SalesReportBuilder()
                .WithTitle("Vendas Anuais")
                .InFormat("HTML")
                .FromPeriod(new DateTime(2024, 1, 1), new DateTime(2024, 12, 31))
                .WithHeader("Painel Executivo")
                .WithFooter("Uso interno")
                .AddColumn("Produto")
                .AddColumn("Quantidade")
                .AddColumn("Valor")
                .WithChart("Pie")
                .AddFilter("Canal=Online")
                .WithSummary()
                .WithTotals()
                .InOrientation("Landscape")
                .Build();

            yearlyHtml.Generate();
        }
    }
}
