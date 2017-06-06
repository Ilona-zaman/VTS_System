using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Business.Interface;
using Core.Data.Model;
using Foundation;
using UIKit;

namespace iOS.Helpers
{
    public class TableSource : UITableViewSource
    {

        IList<Vacation> TableItems;
        private int _filter;
        string CellIdentifier = "TableCell";
        private ILocalizationService _localizationService;

        public TableSource(IList<Vacation> items, int filter)
        {
            TableItems = items;
            _filter = filter;
            _localizationService = AppDelegate.LocalizationService;
        }

        public override nint RowsInSection(UITableView tableview, nint section)
        {
            return GetFilterVacations().Count;
        }

        private IList<Vacation> GetFilterVacations()
        {
            if (_filter == 0)
            {
                return TableItems;
            }
            if (_filter == 1)
            {
                return TableItems.Where(vac => vac.VacationStatusId == 3).ToList();
            }
            if (_filter == 2)
            {
                return TableItems.Where(vac => vac.VacationStatusId == 5).ToList();
            }
            return TableItems;
        }

        public override void RowSelected(UITableView tableView, NSIndexPath indexPath)
        {
            ItemClicked(GetFilterVacations()[indexPath.Row].Id.ToString());
        }
        public override UITableViewCell GetCell(UITableView tableView, NSIndexPath indexPath)
        {
            UITableViewCell cell = tableView.DequeueReusableCell(CellIdentifier);
            Vacation item = GetFilterVacations()[indexPath.Row];

            if (cell == null)
            { cell = new UITableViewCell(UITableViewCellStyle.Value1, CellIdentifier); }

            cell.TextLabel.Text = item.Start.ToString("MMM d") + "-" + item.End.ToString("MMM d");
            cell.TextLabel.TextColor = UIColor.FromRGB(57, 197, 214);
            cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;

            cell.DetailTextLabel.Text = _localizationService.Localize(item.VacationStatusId == 3 ? "Open" : "Close");
            if (item.VacationTypeId == 1)
            {
                cell.ImageView.Image = UIImage.FromBundle("Icon_Request_Green");
                //cell.DetailTextLabel.Text = _localizationService.Localize("Regular");
            }
            if (item.VacationTypeId == 2)
            {
                cell.ImageView.Image = UIImage.FromBundle("Icon_Request_Plum");
                //cell.DetailTextLabel.Text = _localizationService.Localize("Sick");
            }
            if (item.VacationTypeId == 3)
            {
                cell.ImageView.Image = UIImage.FromBundle("Icon_Request_Gray");
                //cell.DetailTextLabel.Text = _localizationService.Localize("Exceptional");
            }
            if (item.VacationTypeId == 4)
            {
                cell.ImageView.Image = UIImage.FromBundle("Icon_Request_Dark");
                //cell.DetailTextLabel.Text = _localizationService.Localize("LeaveWithoutPay");
            }
            if (item.VacationTypeId == 5)
            {
                cell.ImageView.Image = UIImage.FromBundle("Icon_Request_Blue");
                //cell.DetailTextLabel.Text = _localizationService.Localize("Overtime");
            }
            return cell;
        }

        public event Action<string> ItemClicked = delegate { };
    }
}