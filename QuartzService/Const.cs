using System;
using System.Collections.Generic;

namespace QuartzService
{
    public class Const
    {
        public static readonly Guid ЗагрузкаПроизводственныхПоказателей = new("B640DAE1-9CBF-4231-9C48-145DA8EB0240");
        public static readonly Guid РассылкаСуточногоПрогнозаПоГазу = new("1C0AB05D-0462-4821-BB19-34AAA753B4C3");
        public static readonly Guid ЗагрузкаДиспетчерскогоБР = new("E3B9A8A5-3D77-4A80-907F-592D8C31196A");
        public static readonly Guid ЗагрузкаСетевойВоды = new("7E0C882B-71F5-4A32-B277-59EDA0155548");
        public static readonly Guid ЗагрузкаХНПараПХН = new("9C7458D1-1D7F-450C-80BB-5DA719FDAD0B");
        public static readonly Guid ЗагрузкаПТН = new("413460A4-2787-420B-AA06-6C54CB0C2F25");
        public static readonly Guid Моделирование = new("58E74398-321C-4ACE-9F70-80E96A8AD600");
        public static readonly Guid ЗагрузкаМакетов53500 = new("17BE1B5F-2945-40E7-B7A0-BE7019FAC540");
        public static readonly Guid ЗапускWebCервиса = new("0119AF94-F8DE-4F41-BE69-D94F8A3629A0");

        public static readonly Guid Ежечасно = new("B883D885-D06E-4EDD-864A-C32804FFED7A");
        public static readonly Guid Еженедельно = new("4F10F553-1C28-4FB6-95D6-D69374B30864");
        public static readonly Guid Ежемесячно = new("EB0034BF-AE2C-4DC3-BAF7-E145A9028636");
        public static readonly Guid Однократно = new("1C23834D-8826-41F1-9293-EB940559CD4A");
        public static readonly Guid Ежедневно = new("22952C93-479E-441C-BDEF-FC6DB1D8348D");

        public static readonly Guid Задача_Закончена = new("D7435133-9F6A-48DE-B859-21952D3B96F6");
        public static readonly Guid Задача_Запланирована = new("FDBB3784-15D4-4814-9755-AE2CED47A363");
        public static readonly Guid Задача_Выполняется = new("6D3DB96C-8DD6-4257-8A38-C7ECFE518F7D");
        public static readonly Guid Задача_ПриОстановлена = new("57F9DDB3-A4A3-428A-B368-4FF442625F54");
        public static readonly Guid Задача_Остановлена = new("C7E669D7-F095-4272-BEE5-0B75825C0F31");
        public static readonly Guid Задача_ЗаконченаСОшибками = new("58C8B044-CFD1-4510-8876-96CDFF631A6A");
    }
}