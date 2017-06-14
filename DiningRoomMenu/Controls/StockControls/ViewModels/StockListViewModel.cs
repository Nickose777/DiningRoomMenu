using DiningRoomMenu.Logic.DTO.Stock;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiningRoomMenu.Controls.StockControls.ViewModels
{
    public class StockListViewModel : ObservableObject
    {
        public StockListViewModel(IEnumerable<StockDisplayDTO> stocks)
        {
            this.Stocks = new ObservableCollection<StockDisplayDTO>(stocks);
        }

        public ObservableCollection<StockDisplayDTO> Stocks { get; set; }
    }
}
