using Coypu.Finders;

namespace Coypu.Actions
{
    internal class Select : DriverAction
    {
        private readonly DriverScope scope;
        private readonly string locator;
        private readonly string optionToSelect;
        private readonly Options options;
        private readonly DisambiguationStrategy disambiguationStrategy;
        private ElementScope selectElement;

        internal Select(Driver driver, DriverScope scope, string locator, string optionToSelect, DisambiguationStrategy disambiguationStrategy, Options options)
            : base(driver, options)
        {
            this.scope = scope;
            this.locator = locator;
            this.optionToSelect = optionToSelect;
            this.options = options;
            this.disambiguationStrategy = disambiguationStrategy;
        }

        internal Select(Driver driver, ElementScope selectElement, string optionToSelect, DisambiguationStrategy disambiguationStrategy, Options options)
            : base(driver, options)
        {
            this.selectElement = selectElement;
            this.optionToSelect = optionToSelect;
            this.options = options;
            this.disambiguationStrategy = disambiguationStrategy;
        }

        public override void Act()
        {
            selectElement = selectElement ?? FindSelectElement();
            SelectOption(selectElement);
        }

        private SnapshotElementScope FindSelectElement()
        {
            var selectElementFound = disambiguationStrategy.ResolveQuery(new SelectFinder(Driver, locator, scope, options));
            return new SnapshotElementScope(selectElementFound, scope, options);
        }

        void SelectOption(ElementScope selectElementScope)
        {
            var option = disambiguationStrategy.ResolveQuery(new OptionFinder(Driver, optionToSelect, selectElementScope, options));
            Driver.Click(option);
        }
    }
}
