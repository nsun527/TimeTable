﻿#nullable enable

using System;
using System.ComponentModel;
using ZoomDictionary = System.Collections.Generic.Dictionary<string, TimeTableMobile.GGHS.ZoomInfo?>;

// Enables using record types as tuple-like types.
namespace System.Runtime.CompilerServices
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class IsExternalInit { }
}

namespace TimeTableMobile.GGHS
{
    public record ZoomInfo(string? Link, string? Id, string? Pw, string? ClassRoom, string Teacher);

    public interface IZoomLinks
    {
        ZoomDictionary Class1 { get; }
        ZoomDictionary Class2 { get; }
        ZoomDictionary Class3 { get; }
        ZoomDictionary Class4 { get; }
        ZoomDictionary Class5 { get; }
        ZoomDictionary Class6 { get; }
        ZoomDictionary Class7 { get; }
        ZoomDictionary Class8 { get; }
    }

    namespace Grade2.Semester2
    {
        public record ZoomLinks : IZoomLinks
        {
            private record Common
            {
                public static ZoomInfo Reading => new(null, null, null, "https://classroom.google.com/u/0/c/Mzc4NjE5OTMzNjQw", "");
                public static ZoomInfo Mathematics => new(null, null, null, "https://classroom.google.com/u/0/c/MjgzNDM1NjIyODM0", "");


                public static ZoomInfo GlobalEconomics => new("https://zoom.us/j/2521095403?pwd=MVBmOURvRGU1azRwY0lnejVwa2tjUT09", "252 109 5403", "2021", "https://classroom.google.com/c/Mzc4MzkwNjgxMDMy", "김용지");
                public static class Class1to3
                {
                    public static ZoomInfo Reading => Common.Reading with { Link = "https://zoom.us/j/92759524061?pwd=dHUwZllmSEp2NWpRYlVTbmdjODR3dz09", Id = "927 5952 4061", Pw = "0203", Teacher = "백지혜" };
                    public static ZoomInfo Mathematics => Common.Mathematics with { Link = "https://us02web.zoom.us/j/3665089380?pwd=eTM3Ym1UTnpyY1ZQUU8yd2w2SHZPQT09", Id = "366 508 9380", Pw = "201208", Teacher = "김수진" };

                }
                public static class Class4to6
                {
                    public static ZoomInfo Reading => Common.Reading with { Link = "https://zoom.us/j/85644781112?pwd=SC8xWDBZbVRpNW1TSXF6QjRNQk8zQT09", Id = "856 4478 1112", Pw = "0204", Teacher = "윤채영" };
                    public static ZoomInfo Mathematics => Common.Mathematics with { Link = "https://zoom.us/j/7014267742?pwd=eFAvUWQweVdSbmc4Q2JLNGlQWTdsZz09", Id = "701 426 7742", Pw = "1111", Teacher = "박지영" };
                }
                public static class Class7to8
                {
                    public static ZoomInfo Reading => Common.Reading with { Link = "https://zoom.us/j/97317948690?pwd=WWovQUdBQS8rVVMzMUNyZmNPSDY3Zz09", Id = "973 1794 8690", Pw = "626239", Teacher = "임제완" };
                    public static ZoomInfo Mathematics => Common.Mathematics with { Teacher = "공현진" };

                }


                // Advanced English
                // (string? Link, string? Id, string? Pw, string? ClassRoom, string Teacher);
                public static ZoomInfo AdvancedEnglishA1to4 => new("https://zoom.us/j/5472878985?pwd=RlIzSmJnWHBRaWhkRTNkOEJ6UUF5UT09", "547 287 8985", "0512", "https://classroom.google.com/u/0/c/Mjc3NTczMTU4MzE3", "김찬미");
                public static ZoomInfo AdvancedEnglishA5to8 => new("https://zoom.us/j/5031101343?pwd=QXZMWUVoZEkzVnUyY0poanAyclBHdz09", "503 110 1343", "1111", "https://classroom.google.com/u/0/c/Mjc3NTczMTU4MzE3", "김한나");
                public static ZoomInfo AdvancedEnglishB => new(null, null, null, "https://classroom.google.com/u/0/c/Mzc4Mzc4MDg4NzY4", "Lubi");
                public static ZoomInfo AdvancedEnglishC => new("https://zoom.us/j/5365083101?pwd=VDV4VHA5MVUrcDV4cDV1RitZeHovZz09", "536 508 3101", "2021", "https://classroom.google.com/u/0/c/Mjc3NTczMTU4MzE3", "허진");
                public static ZoomInfo AdvancedEnglishD1to2 => new("https://zoom.us/j/5759526245?pwd=akMzYU5kUVpMc202aVkrcWZNRGZSUT09", "575 952 6245", "01020304", "https://classroom.google.com/u/0/c/Mjc3NTczMTU4MzE3", "문선현");
                public static ZoomInfo AdvancedEnglishD3to4 => new("https://zoom.us/j/2277812654?pwd=aytrUzdSRTJSQnNHcTlaUEdJQzFBZz09", "227 781 2654", "11111", "https://classroom.google.com/u/0/c/Mjc3NTczMTU4MzE3", "오지연");
                public static ZoomInfo AdvancedEnglishD5to6 => new("https://zoom.us/my/gghsjennifer?pwd=bkphMW9QeGpXellzczY4d0FCUEFqQT09", "274 535 2030", "20212", "https://classroom.google.com/u/0/c/Mjc3NTczMTU4MzE3", "이경진");
                public static ZoomInfo AdvancedEnglishD7to8 => new("https://zoom.us/j/7936438089?pwd=UkZNaWhFUTE5R2xIYkRxWTRSTC90QT09", "793 643 8089", "1234", "https://classroom.google.com/u/0/c/Mjc3NTczMTU4MzE3", "장종윤");


                public static ZoomInfo LifeAndScience => new("https://zoom.us/j/4153909733?pwd=M2h3eVp5WnNPUTdJdlBwMkd1VGs1dz09", "415 390 9733", "1111", "https://classroom.google.com/u/0/c/Mzc4MzMzMzI1MTA1", "이지원");

                public static ZoomInfo GlobalPolitics3to4 => new("https://zoom.us/j/93720213007?pwd=NVhibHlFdk5QK21uenZFdUhDb3VwQT09", "937 2021 3007", "204", "https://classroom.google.com/u/0/c/MzA1OTAyOTM5OTM2", "엄혜용");
                public static ZoomInfo GlobalPolitics1and8 => new("https://zoom.us/j/4613355190?pwd=WEZzaFhtTVpDZVk4L09XK1VlQ3Z5UT09", "461 335 5190", "1111", "https://classroom.google.com/u/0/c/MzA1OTAyOTM5OTM2", "정슬기");
                public static ZoomInfo CompareCultureA => new("https://zoom.us/j/6941791038?pwd=TUJLRnZEVEhMNGVhRGxtaVBXb3BNZz09", "694 179 1038", "2021", "https://classroom.google.com/u/0/c/Mzc4NzczNjQ1MDU0", "홍정민");
                public static ZoomInfo CompareCultureB => new("https://zoom.us/j/5467290895?pwd=dUFHMDlEdjk3VzV6dnl0OERCVzlqQT09", "546 729 0895", "2030", "https://classroom.google.com/u/0/c/Mzc4NzczNjQ1MDU0", "정혜영");


                public static ZoomInfo PoliticsPhilosophy => new("https://zoom.us/j/9401959597", "940 195 9597", "255226", null, "류제광");
                public static ZoomInfo RegionResearch => new("https://zoom.us/j/96926016442", "969 2601 6442", "geogeo", null, "조한솔");
                public static ZoomInfo GISAnalyze => new("https://us02web.zoom.us/j/9055851564?pwd=RENhem04T0JsTEZzNDhBQk4xa3JRUT09", "905 585 1564", "2121", "https://classroom.google.com/u/0/c/Mzc1MzAxOTg3NjM0", "이건");
                /*
public static string ScienceHistory => "과학사";
public static string LifeAndScience => "생활과 과학";

public static string GlobalEconomics => "국제경제";
public static string GlobalPolitics => "국제정치";

public static string EasternHistory => "동양근대사";
public static string HistoryAndCulture => "세계 역사와 문화";
*/
                // Language
                // public static ZoomInfo Spanish => new("https://us02web.zoom.us/j/7411091130?pwd=M0tZTXJwaFRXb3RmZm1jODNhdUtqUT09");
                public static ZoomInfo Chinese => new("https://zoom.us/j/99535123743?pwd=d0dPemVjNXIxcks5RCt0OFc1aGg0Zz09", "995 3512 3743", "1eMXJM", "https://classroom.google.com/u/0/c/MjgzOTg5NzIyMjQ5", "김나연");
                public static ZoomInfo Japanese => new("https://zoom.us/j/2298991005?pwd=ZUpFSWdlTHdleTZzSEVSeEZSTDlzQT09", "229 899 1005", "105", "https://classroom.google.com/c/MjgyNjA1Nzc5MjAy", "하지원");

                // Others
                public static ZoomInfo CreativeSolve => new("https://meet.google.com/lookup/bgt6c65ccm", null, null, "https://classroom.google.com/u/0/c/Mjc3MDAwNjYxOTcx", "조광진");
                public static ZoomInfo MathResearch2 => new("https://zoom.us/j/6749678415?pwd=ZVpqNmNHMDE3MGZKblF2NnptMzVvQT09", "674 967 8415", "fbpY9j", "https://classroom.google.com/u/0/c/Mjg0MTE0ODQ2Njkx", "김수진");
                public static ZoomInfo MathResearch5to8 => new(null, null, null, "https://classroom.google.com/u/0/c/MjgzNTMyMTc3NDgw", "공현진");
                public static ZoomInfo Sport1to4 => new("https://zoom.us/j/3373011774?pwd=QWYybW1SMlljVlVFRzRTY0RvenVrUT09", "337 301 1774", "123456", null, "허진용");
                public static ZoomInfo Sport5to8 => new("https://zoom.us/j/3225620828?pwd=NHBqdnZrZnZCMTNpRmlCakhVUVJPZz09", "322 562 0828", "1234", null, "윤보경");
            }

            public static class CompareCultures
            {
                public static class Hong
                {
                    public static string Zoom => Common.CompareCultureA.Link!;
                    public static string Id => Common.CompareCultureA.Id!;
                    public static string Password => Common.CompareCultureA.Pw!;
                }
                public static class Jung
                {
                    public static string Zoom => Common.CompareCultureB.Link!;
                    public static string Id => Common.CompareCultureB.Id!;
                    public static string Password => Common.CompareCultureB.Pw!;
                }
            }

            public ZoomDictionary Class1 => new()
            {
                [Subjects.CellName.Reading] = Common.Class1to3.Reading,

                [Subjects.CellName.Mathematics] = Common.Class1to3.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA1to4,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD1to2,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,

                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics1and8,
                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,


                [Subjects.CellName.RegionResearch] = Common.RegionResearch,
                [Subjects.CellName.GISAnalyze] = Common.GISAnalyze,

                [Subjects.CellName.Sport] = Common.Sport1to4,
            };

            public ZoomDictionary Class2 => new()
            {
                [Subjects.CellName.Reading] = new ZoomInfo("https://zoom.us/j/97128678499?pwd=WXdXdHFkOC9mNlJQUGZtTS91SU8rdz09", "971 2867 8499", "0202", "https://classroom.google.com/u/0/c/Mzc4NjE5OTMzNjQw", "백지혜"),

                [Subjects.CellName.Mathematics] = Common.Class1to3.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA1to4,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD1to2,

                [Subjects.CellName.Chinese] = Common.Chinese,
                [Subjects.CellName.Japanese] = Common.Japanese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
                [Subjects.CellName.MathResearch] = Common.MathResearch2,

                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics1and8,

                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,

                [Subjects.CellName.RegionResearch] = Common.RegionResearch,
                [Subjects.CellName.GISAnalyze] = Common.GISAnalyze,

                [Subjects.CellName.Sport] = Common.Sport1to4,
            };

            public ZoomDictionary Class3 => new()
            {
                [Subjects.CellName.Reading] = Common.Class1to3.Reading,

                [Subjects.CellName.Mathematics] = Common.Class1to3.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA1to4,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD3to4,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,


                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics3to4,

                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,

                [Subjects.CellName.RegionResearch] = Common.RegionResearch,
                [Subjects.CellName.GISAnalyze] = Common.GISAnalyze,

                [Subjects.CellName.Sport] = Common.Sport1to4,
            };


            public ZoomDictionary Class4 => new()
            {
                [Subjects.CellName.Reading] = Common.Class4to6.Reading,

                [Subjects.CellName.Mathematics] = Common.Class4to6.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA1to4,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD3to4,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,


                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics3to4,

                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,

                [Subjects.CellName.RegionResearch] = Common.RegionResearch,
                [Subjects.CellName.GISAnalyze] = Common.GISAnalyze,

                [Subjects.CellName.Sport] = Common.Sport1to4,
            };

            public ZoomDictionary Class5 => new()
            {
                [Subjects.CellName.Reading] = Common.Class4to6.Reading,

                [Subjects.CellName.Mathematics] = Common.Class4to6.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA5to8,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD5to6,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
                [Subjects.CellName.MathResearch] = Common.MathResearch5to8,

                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                // [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics,

                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,

                [Subjects.CellName.RegionResearch] = Common.RegionResearch,
                [Subjects.CellName.GISAnalyze] = Common.GISAnalyze,

                [Subjects.CellName.Sport] = Common.Sport5to8,
            };

            public ZoomDictionary Class6 => new()
            {
                [Subjects.CellName.Reading] = Common.Class4to6.Reading,

                [Subjects.CellName.Mathematics] = Common.Class4to6.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA5to8,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD5to6,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
                [Subjects.CellName.MathResearch] = Common.MathResearch5to8,

                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                // [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics,

                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,

                [Subjects.CellName.RegionResearch] = Common.RegionResearch,
                [Subjects.CellName.GISAnalyze] = Common.GISAnalyze,

                [Subjects.CellName.Sport] = Common.Sport5to8
            };

            public ZoomDictionary Class7 => new()
            {
                [Subjects.CellName.Reading] = Common.Class7to8.Reading,

                [Subjects.CellName.Mathematics] = Common.Class7to8.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA5to8,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD7to8,

                [Subjects.CellName.Chinese] = Common.Chinese,
                [Subjects.CellName.Japanese] = Common.Japanese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
                [Subjects.CellName.MathResearch] = Common.MathResearch5to8,

                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                // [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics,

                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,

                [Subjects.CellName.RegionResearch] = Common.RegionResearch,
                [Subjects.CellName.GISAnalyze] = Common.GISAnalyze,

                [Subjects.CellName.Sport] = Common.Sport5to8
            };

            public ZoomDictionary Class8 => new()
            {
                [Subjects.CellName.Reading] = Common.Class7to8.Reading,

                [Subjects.CellName.Mathematics] = Common.Class7to8.Mathematics,

                [Subjects.CellName.AdvancedEnglish + "A"] = Common.AdvancedEnglishA5to8,
                [Subjects.CellName.AdvancedEnglish + "B"] = Common.AdvancedEnglishB,
                [Subjects.CellName.AdvancedEnglish + "C"] = Common.AdvancedEnglishC,
                [Subjects.CellName.AdvancedEnglish + "D"] = Common.AdvancedEnglishD7to8,

                [Subjects.CellName.Chinese] = Common.Chinese,

                [Subjects.CellName.CreativeSolve] = Common.CreativeSolve,
                [Subjects.CellName.MathResearch] = Common.MathResearch5to8,

                [Subjects.CellName.LifeAndScience] = Common.LifeAndScience,

                // [Subjects.CellName.GlobalPolitics] = Common.GlobalPolitics,

                [Subjects.CellName.GlobalEconomics] = Common.GlobalEconomics,
                [Subjects.CellName.CompareCulture + "A"] = Common.CompareCultureA,
                [Subjects.CellName.CompareCulture + "B"] = Common.CompareCultureB,

                [Subjects.CellName.PoliticsPhilosophy] = Common.PoliticsPhilosophy,
                [Subjects.CellName.RegionResearch] = Common.RegionResearch,
                [Subjects.CellName.GISAnalyze] = Common.GISAnalyze,

                [Subjects.CellName.Sport] = Common.Sport5to8
            };
        }
    }
}