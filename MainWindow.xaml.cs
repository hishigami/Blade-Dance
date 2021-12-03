using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BladeDance
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        // Constant point values
        private const int homework = 15;
        private const int kfc = 150;
        private const int ocun = 200;
        private const int raidy = 250;
        private const int blacked = 300;
        private const int boxCost = 2000;

        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Driver function to determine the user's current pacing.
        /// If there is enough surplus RP, the number of obtainable Completion Boxes will also be computed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalculateClick(object sender, RoutedEventArgs e)
        {
            // Variable definitions
            int goal;
            string output;

            int weeks = (int)(intTime.Value);
            int currentPoints = (int)(intPoints.Value);
            int dailies = (int)(intNormals.Value);
            int isys = int.Parse(cmbIsys.Text);
            int oculus = int.Parse(cmbOculus.Text);
            int raids = int.Parse(cmbRaids.Text);
            int cult = int.Parse(cmbCult.Text);

            // Set goal based on calculator mode
            if ((bool)RadioMythic.IsChecked)
            {
                goal = 13500;
            }
            // Full Clear was selected
            else
            {
                goal = 21600;
            }

            // Determine if the current pace is enough to meet the specified goal
            int futurePoints = currentPoints + weeks * (dailies * homework * 7 + isys * kfc + oculus * ocun + raids * raidy + cult * blacked);
            int pointDiff = futurePoints - goal;
            // Not enough points
            if (pointDiff < 0)
            {
                pointDiff *= -1;
                output = "You won't have enough points to meet the target " + goal.ToString() + " RP! \n" +
                    "You would be " + pointDiff.ToString() + " RP behind schedule. \n" +
                    "Please consider running more weekly content, or rush event Mirkwood runs if absolutely necessary.";
            }
            // Goal reached
            else
            {
                output = "You will be able to meet the target at your current pace. \n" +
                    "You would have " + pointDiff.ToString() + " RP leftover. \n";
                // Not enough points to get any boxes, however
                if (pointDiff < boxCost)
                {
                    output += "However, this would not be enough to buy any Completion Boxes.";
                }
                // Enough points leftover to purchase box(es)
                else
                {
                    int boxcount = pointDiff / boxCost;
                    output += "Additionally, you will be able to purchase up to " + boxcount.ToString() + " Completion Boxes. \n";
                    /* There are 35 possible Mythics to roll, so up to 34 Conversion Stones are required to ban all but your desired Mythic per roll.
                     * Given there are 3 stones from reaching Level 100, 6 from Training Level 10, 8 from Posia's shop per week, and 5 from meeting either Path goal,
                     * this means you can get 22 stones *under ideal playing habits*, while the other 12 must be obtained from Completion Boxes.
                     * This will allow you to roll your desired Mythic if you manage to not roll it *at all* in the interrim.
                     */
                    if (boxcount >= 12)
                    {
                        output += "This is (more than) enough Completion Boxes to guarantee your desired Mythic if you " +
                            "obtained the other 22 Mythic Conversion Stones throughout the event!";
                    }

                }
            }
            // Print results
            txtOutput.Text = output;
        }

        /// <summary>
        /// Reverts all fields to their default values.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ResetClick(object sender, RoutedEventArgs e)
        {
            intTime.Value = 8;
            intPoints.Value = 0;

            intNormals.Value = 0;
            cmbIsys.SelectedIndex = 0;
            cmbOculus.SelectedIndex = 0;
            cmbRaids.SelectedIndex = 0;
            cmbCult.SelectedIndex = 0;

            RadioMythic.IsChecked = true;

            txtOutput.Text = ("All fields have been reset to their defaults.");
        }
    }
}
