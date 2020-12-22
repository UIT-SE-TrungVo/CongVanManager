using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CongVanManager.ViewModel
{
    abstract class FilterSetting : ObservableObject
    {
        public abstract bool Filter(CongVan cv);

        public bool Match(CongVan cv, string MatchText)
        {
            if (MatchText is null || MatchText == "")
                return true;

            List<string> matchItem = new List<string>();
            string[] strSplit = MatchText.Split('"');
            for (int i = 0; i < strSplit.Count(); i++)
            {
                if (strSplit[i] == "")
                    continue;
                if (i % 2 == 1)
                    matchItem.Add(strSplit[i]);
                else
                {
                    string[] wordSplit = strSplit[i].Split(' ');
                    foreach (string word in wordSplit)
                        matchItem.Add(word);
                }
            }
            
            foreach (string word in matchItem)
            {
                if (cv.GhiChu != null && cv.GhiChu.ToLower().Contains(word.ToLower()))
                    return true;
                if (cv.LoaiCongVan != null && cv.LoaiCongVan.Id.Contains(word.ToUpper()))
                    return true;
                if (cv.SoKyHieu == word)
                    return true;
                if (cv.TrichYeu != null && cv.TrichYeu.ToLower().Contains(word.ToLower()))
                    return true;
                if (cv.Status != null && cv.Status.ToLower().Contains(word.ToLower()))
                    return true;
                if (cv.NoiGui != null && cv.NoiGui.Name.ToLower().Contains(word.ToLower()))
                    return true;
                if (cv.NoiGui != null && cv.NoiGui.Email == word)
                    return true;
                foreach (var noiNhan in cv.DanhSachNoiNhan)
                {
                    if (noiNhan.LienHe == null)
                        continue;
                    if (noiNhan.LienHe.Name.ToLower().Contains(word.ToLower()))
                        return true;
                    if (noiNhan.LienHe.Email == word)
                        return true;
                }
            }
            return false;
        }
    }
}
