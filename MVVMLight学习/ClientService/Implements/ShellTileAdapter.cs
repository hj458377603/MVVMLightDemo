using Microsoft.Phone.Shell;
using PhoneClient.Adapters.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneClient.Adapters.Implements
{
    public class ShellTileAdapter : IShellTileService
    {
        #region 字段

        private ShellTile shelltile;

        #endregion

        #region 属性

        /// <summary>
        /// 固定到“开始”的应用程序图块的集合。
        /// </summary>
        public IEnumerable<ShellTile> ActiveTiles
        {
            get
            {
                return ShellTile.ActiveTiles;
            }
        }

        /// <summary>
        /// 点按次要图块时导航到的 URI 和启动参数
        /// </summary>
        public Uri NavigationUri
        {
            get
            {
                return shelltile.NavigationUri;
            }
        }

        #endregion

        #region 构造函数

        public ShellTileAdapter(ShellTile shelltile)
        {
            this.shelltile = shelltile;
        }

        #endregion

        #region 方法

        public void Create(Uri navigationUri, ShellTileData initialData)
        {
            ShellTile.Create(navigationUri, initialData);
        }

        public void Delete()
        {
            shelltile.Delete();
        }

        public void Update(ShellTileData data)
        {
            shelltile.Update(data);
        }

        #endregion
    }
}
