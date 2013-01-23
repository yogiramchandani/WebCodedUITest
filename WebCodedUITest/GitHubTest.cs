using System;
using Microsoft.VisualStudio.TestTools.UITesting;
using Microsoft.VisualStudio.TestTools.UITesting.HtmlControls;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace WebCodedUITest
{
    [CodedUITest]
    public class GitHubTest
    {
        private GitHubHomePage _githubHomePageWindow;
        public GitHubHomePage GitHubHomePageWindow
        {
            get
            {
                if ((_githubHomePageWindow == null))
                {
                    var browser = new BrowserWindow();
                    browser.SearchProperties[UITestControl.PropertyNames.Name] = "yogiramchandani (Yogi Ramchandani) · GitHub";
                    _githubHomePageWindow = new GitHubHomePage(browser);
                }
                return _githubHomePageWindow;
            }
        }

        [TestMethod]
        public void change_contribution_list_selection_to_3days()
        {
            GitHubHomePageWindow.LaunchPage();
            HtmlSpan periodDropDownList = GitHubHomePageWindow.ContributionActivity.PeriodDropDownList;

            HtmlDiv item3Days = GitHubHomePageWindow.ContributionActivity.MenuItem3Days;

            Assert.AreEqual("Period:1 Week", periodDropDownList.InnerText);

            Mouse.Click(periodDropDownList);
            Mouse.Click(item3Days);

            Assert.AreEqual("Period:3 Days", periodDropDownList.InnerText);
        }

        [TestMethod]
        public void graph_cell_zero_is_a_year_ago_from_now()
        {
            GitHubHomePageWindow.LaunchPage();
            HtmlCustom day0 = GitHubHomePageWindow.CalendarGraph[0];
            DateTime expectedDate = DateTime.Now.AddYears(-1);
            Assert.IsTrue(day0.InnerText.Contains(expectedDate.ToString("yyyy")), string.Format("expected Year {0}, actual year {1}", expectedDate.ToString("yyyy"), GitHubHomePageWindow.CalendarGraph[6].InnerText));
            Assert.IsTrue(day0.InnerText.Contains(expectedDate.ToString("MMMM")), string.Format("expected Month {0}, actual month {1}", expectedDate.ToString("MMMM"), GitHubHomePageWindow.CalendarGraph[6].InnerText));
            Assert.IsTrue(day0.InnerText.Contains(expectedDate.ToString("dd")), string.Format("expected Day {0}, actual day {1}", expectedDate.ToString("dd"), GitHubHomePageWindow.CalendarGraph[6].InnerText));
        }
    }
}