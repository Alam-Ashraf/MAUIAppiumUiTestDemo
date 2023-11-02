namespace MAUIAppiumUiTestDemo;

public partial class HomePage : ContentPage
{
	public HomePage()
	{
		InitializeComponent();
	}

    void OnAddClicked(System.Object sender, System.EventArgs e)
    {
        if (IsValid())
        {
            int fNo = Convert.ToInt32(Num1.Text.ToString());
            int sNo = Convert.ToInt32(Num2.Text.ToString());

            ResultTxt.Text = (fNo + sNo).ToString();
        }
    }

    void OnMultiplyClicked(System.Object sender, System.EventArgs e)
    {
        if (IsValid())
        {
            int fNo = Convert.ToInt32(Num1.Text.ToString());
            int sNo = Convert.ToInt32(Num2.Text.ToString());

            ResultTxt.Text = (fNo * sNo).ToString();
        }
    }

    private bool IsValid()
    {
        if (string.IsNullOrEmpty(Num1.Text))
        {
            ResultTxt.Text = "Please enter first number.";
            return false;
        }
        else if (string.IsNullOrEmpty(Num2.Text))
        {
            ResultTxt.Text = "Please enter second number.";
            return false;
        }

        return true;
    }
}