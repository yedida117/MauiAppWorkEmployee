



using MauiAppWorkEmployee.ViewModel;

namespace MauiAppWorkEmployee.Views
{
   
    public partial class AddWorkPage2 : ContentPage
    {
        public AddWorkPage2(AddWorkVm vm)
        {
            InitializeComponent();
            BindingContext = vm;    
        }
    }
}