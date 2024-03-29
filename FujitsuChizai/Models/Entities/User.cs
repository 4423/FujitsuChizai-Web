﻿using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace FujitsuChizai.Models.Entities
{
    /// <summary>
    /// ユーザ情報を表します。
    /// </summary>
    public class User
    {
        /// <summary>
        /// ユーザID
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 生まれ年
        /// </summary>
        public int BornIn { get; set; }
        /// <summary>
        /// 性別
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Sexes Sex { get; set; }
        /// <summary>
        /// 出身国
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public Countries Country { get; set; }
        /// <summary>
        /// 登録日時
        /// </summary>
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual ICollection<History> Histories { get; set; }
    }

    /// <summary>
    /// 性別を表します。
    /// この性別は ISO/IEC 5218 に基きます。
    /// </summary>
    public enum Sexes
    {
        Unknown,
        Male,
        Female,
        NotApplicable = 9
    }

    // 193 + 4 - 1 = 196国
    // http://www.mofa.go.jp/mofaj/files/000023536.pdf
    /// <summary>
    /// 国を表します。
    /// </summary>
    public enum Countries
    {
        Afghanistan,
        Albania,
        Algeria,
        Andorra,
        Angola,
        AntiguaAndBarbuda,
        Argentina,
        Armenia,
        Australia,
        Austria,
        Azerbaijan,
        Bahamas,
        Bahrain,
        Bangladesh,
        Barbados,
        Belarus,
        Belgium,
        Belize,
        Benin,
        Bhutan,
        Bolivia,
        BosniaAndHerzegovina,
        Botswana,
        Brazil,
        BruneiDarussalam,
        Bulgaria,
        BurkinaFaso,
        Burundi,
        Cambodia,
        Cameroon,
        Canada,
        CapeVerde,
        CentralAfricanRepublic,
        Chad,
        Chile,
        China,
        Colombia,
        Comoros,
        Congo,
        CookIslands,
        CostaRica,
        CoteDIvoire,
        Croatia,
        Cuba,
        Cyprus,
        CzechRepublic,
        DemocraticRepublicOfTheCongo,
        Denmark,
        Djibouti,
        Dominica,
        DominicanRepublic,
        Ecuador,
        Egypt,
        ElSalvador,
        EquatorialGuinea,
        Eritrea,
        Estonia,
        Ethiopia,
        Fiji,
        Finland,
        France,
        Gabon,
        Gambia,
        Georgia,
        Germany,
        Ghana,
        Greece,
        Grenada,
        Guatemala,
        Guinea,
        GuineaBissau,
        Guyana,
        Haiti,
        Honduras,
        Hungary,
        Iceland,
        India,
        Indonesia,
        Iran,
        Iraq,
        Ireland,
        Israel,
        Italy,
        Jamaica,
        Japan,
        Jordan,
        Kazakhstan,
        Kenya,
        Kiribati,
        Kuwait,
        Kyrgyzstan,
        LaoPeoplesDemocraticRepublic,
        Latvia,
        Lebanon,
        Lesotho,
        Liberia,
        Libya,
        Liechtenstein,
        Lithuania,
        Luxembourg,
        Madagascar,
        Malawi,
        Malaysia,
        Maldives,
        Mali,
        Malta,
        MarshallIslands,
        Mauritania,
        Mauritius,
        Mexico,
        Micronesia,
        Monaco,
        Mongolia,
        Montenegro,
        Morocco,
        Mozambique,
        Myanmar,
        Namibia,
        Nauru,
        Nepal,
        Netherlands,
        NewZealand,
        Nicaragua,
        Niger,
        Nigeria,
        Niue,
        Norway,
        Oman,
        Pakistan,
        Palau,
        Panama,
        PapuaNewGuinea,
        Paraguay,
        Peru,
        Philippines,
        Poland,
        Portugal,
        Qatar,
        RepublicOfKorea,
        RepublicOfKosovo,
        RepublicOfMoldova,
        Romania,
        RussianFederation,
        Rwanda,
        SaintKittsAndNevis,
        SaintLucia,
        SaintVincentAndtheGrenadines,
        Samoa,
        SanMarino,
        SaoTomeAndPrincipe,
        SaudiArabia,
        Senegal,
        Serbia,
        Seychelles,
        SierraLeone,
        Singapore,
        Slovakia,
        Slovenia,
        SolomonIslands,
        Somalia,
        SouthAfrica,
        SouthSudan,
        Spain,
        SriLanka,
        Sudan,
        Suriname,
        Swaziland,
        Sweden,
        Switzerland,
        Syria,
        Tajikistan,
        Thailand,
        TheFormarYugoslavRepublicOfMacedonia,
        TimorLeste,
        Togo,
        Tonga,
        TrinidadAndTobago,
        Tunisia,
        Turkey,
        Turkmenistan,
        Tuvalu,
        Uganda,
        Ukraine,
        UnitedArabEmirates,
        UnitedKingdom,
        UnitedOfRepublicOfTanzania,
        UnitedStates,
        Uruguay,
        Uzbekistan,
        Vanuatu,
        Vatican,
        Venezuela,
        VietNam,
        Yemen,
        Zambia,
        Zimbabwe
    }
}