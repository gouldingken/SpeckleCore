﻿using System.Windows;
using System.Windows.Input;
using System.Linq;
using SpeckleCore;

namespace SpecklePopup
{
  public partial class MainWindow : Window
  {
    public string restApi;
    public string apitoken;

    public string selectedEmail;
    public string selectedServer;
    public bool HasDefaultAccount = false;


    public MainWindow(bool autoClose, bool showButtons)
    {
      InitializeComponent();

      //make the "use selected" button visible
      if (showButtons)
      {
        AccountsControl.ButonUseSelected.Visibility = Visibility.Visible;
        AccountsControl.ButonUseSelected.Click += ButtonUseSelected_Click;

        //if autoclose and there is a defaut account
        if (autoClose && AccountsControl.accounts.Any(x => x.IsDefault))
        {
          UseSelected(AccountsControl.accounts.First(x => x.IsDefault));
          HasDefaultAccount = true;
          this.Close();
        }
      }
      else
      {
       // AccountsControl.ButonUseSelected.Visibility = Visibility.Collapsed;
      }


      this.WindowStartupLocation = WindowStartupLocation.CenterScreen;
      this.DragRectangle.MouseDown += (sender, e) =>
      {
        this.DragMove();
      };

    }

    private void Rectangle_MouseDown(object sender, MouseButtonEventArgs e)
    {
      this.DragMove();
    }

    private void CancelButton_Click(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    private void ButtonUseSelected_Click(object sender, RoutedEventArgs e)
    {
      if (!(AccountsControl.AccountListBox.SelectedIndex != -1))
      {
        MessageBox.Show("Please select an account first.");
        return;
      }
      UseSelected(AccountsControl.accounts[AccountsControl.AccountListBox.SelectedIndex]);
      this.Close();
    }

    private void UseSelected(Account account)
    {
      restApi = account.RestApi;
      apitoken = account.Token;
      selectedEmail = account.Email;
      selectedServer = account.ServerName;
    }

  }
}
