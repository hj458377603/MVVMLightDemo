using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneClient.Adapters.Interfaces
{
    public interface IShellTileService
    {
        #region 属性

        IEnumerable<ShellTile> ActiveTiles { get; }
        Uri NavigationUri { get; }

        #endregion

        #region 方法

        void Create(Uri navigationUri, ShellTileData initialData);
        void Delete();
        void Update(ShellTileData data);

        #endregion

    }
}
