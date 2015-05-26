using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.ViewModel
{
    public class TeaEditorViewModel:ViewModelBase
    {
        public TeaEditorViewModel()
        {
            BaseTeaViewModel = new ItemEditorViewModel("Base Tea");
            FlavorViewModel = new ItemEditorViewModel("Flavor");
            ToppingsViewModel = new ItemEditorViewModel("Toppings");
        }

        public ItemEditorViewModel BaseTeaViewModel { get; set; }
        public ItemEditorViewModel FlavorViewModel { get; set; }
        public ItemEditorViewModel ToppingsViewModel { get; set; }
    }
}
