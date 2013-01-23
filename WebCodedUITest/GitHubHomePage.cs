using System;
using System.Globalization;
using Microsoft.VisualStudio.TestTools.UITest.Extension;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;

namespace WebCodedUITest
{
    public class GitHubHomePage : HtmlDocument
    {
        private CalendarGraph _calendarGraphCustom;
        private ContributionActivity _contributionActivity;

        public GitHubHomePage(BrowserWindow browser) : base(browser)
        {
            FilterProperties[PropertyNames.PageUrl] = "https://github.com/yogiramchandani";
        }

        public void LaunchPage()
        {
            BrowserWindow bw = BrowserWindow.Launch("https://github.com/yogiramchandani");
            bw.WaitForControlReady();
        }

        public ContributionActivity ContributionActivity
        {
            get
            {
                if ((_contributionActivity == null))
                {
                    _contributionActivity = new ContributionActivity(this);
                }
                return _contributionActivity;
            }
        }

        public CalendarGraph CalendarGraph
        {
            get
            {
                if ((_calendarGraphCustom == null))
                {
                    _calendarGraphCustom = new CalendarGraph(this);
                }
                return _calendarGraphCustom;
            }
        }
    }

    public class ContributionActivity : HtmlDiv
    {
        private HtmlDiv _1DayItem;
        private HtmlDiv _3DaysItem;
        private HtmlSpan _periodList;

        public ContributionActivity(UITestControl searchLimitContainer)
            : base(searchLimitContainer)
        {
            SearchProperties[HtmlControl.PropertyNames.Id] = "contribution-activity";
        }

        public HtmlSpan PeriodDropDownList
        {
            get
            {
                if ((_periodList == null))
                {
                    _periodList = new HtmlSpan(this);
                    _periodList.SearchConfigurations.Add(SearchConfiguration.VisibleOnly);
                    _periodList.SearchProperties.Add(new PropertyExpression("InnerText", "Period:", PropertyExpressionOperator.Contains));
                    //_periodList.FilterProperties[HtmlControl.PropertyNames.InnerText] = "Period:";
                    _periodList.FilterProperties[HtmlControl.PropertyNames.Class] = "minibutton select-menu-button js-menu-target";
                }
                return _periodList;
            }
        }

        public HtmlDiv MenuItem1Day
        {
            get
            {
                if ((_1DayItem == null))
                {
                    _1DayItem = new HtmlDiv(this);
                    _1DayItem.SearchConfigurations.Add(SearchConfiguration.VisibleOnly);
                    _1DayItem.SearchProperties.Add(new PropertyExpression("InnerText", "1 Day", PropertyExpressionOperator.Contains));
                    _1DayItem.FilterProperties[HtmlControl.PropertyNames.Class] = "select-menu-item-text js-select-button-text";
                }
                return _1DayItem;
            }
        }

        public HtmlDiv MenuItem3Days
        {
            get
            {
                if ((_3DaysItem == null))
                {
                    _3DaysItem = new HtmlDiv(this);
                    _3DaysItem.SearchConfigurations.Add(SearchConfiguration.VisibleOnly);
                    _3DaysItem.SearchProperties.Add(new PropertyExpression("InnerText", "3 Days", PropertyExpressionOperator.Contains));
                    _3DaysItem.FilterProperties[HtmlControl.PropertyNames.Class] = "select-menu-item-text js-select-button-text";
                }
                return _3DaysItem;
            }
        }
    }

    public class CalendarGraph : HtmlCustom
    {
        public CalendarGraph(UITestControl searchLimitContainer) : base(searchLimitContainer)
        {
            SearchProperties["Id"] = "calendar-graph";
        }

        private HtmlCustom[] _graphCell = new HtmlCustom[365];
        public HtmlCustom this[int day]
        {
            get
            {
                if (day >= 0 && day < _graphCell.Length)
                {
                    if (_graphCell[day] == null)
                    {
                        _graphCell[day] = new HtmlCustom(this);
                        _graphCell[day].FilterProperties["Class"] = "day";
                        _graphCell[day].FilterProperties["TagInstance"] = day.ToString(CultureInfo.InvariantCulture);
                    }
                    return _graphCell[day];
                }
                return null;
            }
        }
    }
}