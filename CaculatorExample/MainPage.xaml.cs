namespace CaculatorExample
{
    public partial class MainPage : ContentPage
    {
        private string _currentOperator;
        private double _firstNumber;
        private bool _isSecondNumber;
        private string _calculationText; // To hold the current calculation

        public MainPage()
        {
            InitializeComponent();
            _calculationText = string.Empty;
        }

        // Method to handle number button clicks
        private void OnNumberClicked(object sender, EventArgs e)
        {
            var button = (Button)sender;
            var number = button.Text;

            if (_isSecondNumber)
            {
                Display.Text = number;  // Start with the second number fresh
                _isSecondNumber = false;
            }
            else
            {
                Display.Text = (Display.Text == "0") ? number : Display.Text + number;  // Continue appending the number
            }

            // Update the calculation text to show the current input
            _calculationText += number;
            UpdateDisplay();
        }

        // Method to handle operator button clicks
        private void OnOperatorClicked(string operatorSymbol)
        {
            _currentOperator = operatorSymbol;
            _firstNumber = double.Parse(Display.Text);  // Save the first number for later calculation
            _isSecondNumber = true;

            // Update the calculation text to show the operator
            _calculationText += " " + operatorSymbol + " ";
            UpdateDisplay();
        }

        private void OnAddClicked(object sender, EventArgs e) => OnOperatorClicked("+");
        private void OnSubtractClicked(object sender, EventArgs e) => OnOperatorClicked("-");
        private void OnMultiplyClicked(object sender, EventArgs e) => OnOperatorClicked("*");
        private void OnDivideClicked(object sender, EventArgs e) => OnOperatorClicked("/");

        // Equals operation logic
        private void OnEqualsClicked(object sender, EventArgs e)
        {
            // Extract the second number from the calculation string
            double secondNumber = GetSecondNumber();  // This gets the second number after the operator
            double result = 0;

            // Perform the calculation based on the operator
            switch (_currentOperator)
            {
                case "+":
                    result = _firstNumber + secondNumber;
                    break;
                case "-":
                    result = _firstNumber - secondNumber;
                    break;
                case "*":
                    result = _firstNumber * secondNumber;
                    break;
                case "/":
                    result = _firstNumber / secondNumber;
                    break;
            }

            // Display the result and clear the calculation string
            Display.Text = result.ToString();
            _calculationText = string.Empty;
        }

        // Method to extract the second number after the operator
        private double GetSecondNumber()
        {
            var operatorIndex = _calculationText.IndexOf(_currentOperator);
            var secondNumberStr = _calculationText.Substring(operatorIndex + 2);  // Get the part after the operator
            return double.Parse(secondNumberStr);
        }

        // Clear the calculator
        private void OnClearClicked(object sender, EventArgs e)
        {
            _firstNumber = 0;
            _isSecondNumber = false;
            _calculationText = string.Empty;
            Display.Text = "0";
        }

        // Update the display to show the current calculation
        private void UpdateDisplay()
        {
            // Display the current equation (e.g., "1 + 2")
            Display.Text = _calculationText;
        }
    }
}

