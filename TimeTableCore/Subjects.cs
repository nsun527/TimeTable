﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace TimeTableCore
{
    public class Subject
    {
        public Subject(string name) => FullName = name;
        public Subject(string name, string shortName)
        {
            FullName = name;
            ShortName = shortName;
        }
        public Subject(in Subject subject)
        {
            FullName = subject.FullName;
            ShortName = subject.ShortName;
        }

        public static implicit operator string(Subject subject) => subject.Name;
        public virtual string FullName { get; } = string.Empty;
        public virtual string? ShortName { get; } = null;
        public virtual string Name => ShortName ?? FullName;
        public bool IsSameWith(string name)
        {
            if (name == ShortName)
                return true;
            if (name == FullName)
                return true;
            return false;
        }

        private Subject AddPostFix(char ch) 
            => ShortName is null ? new(FullName + ch) : new(FullName + ch, ShortName + ch);
        public Subject A() => AddPostFix('A');
        public Subject B() => AddPostFix('B');
        public Subject C() => AddPostFix('C');
        public Subject D() => AddPostFix('D');
    }

    public interface ISelectiveSubject
    {
        // Set with given str. If none is matched, ignored.
        void SetAs(string subject);
    }

    public class Korean : Subject, ISelectiveSubject
    {
        public static Subject LangMedia => new("언어와 매체", "언매");
        public static Subject SpeechWriting => new("화법과 작문", "화작");
        public static Subject Default => new("국어");
        public static Subject Selected { get; set; } = Default; // set 변경
        public Korean() : base(Default) { }
        public Korean(in Subject korean) : base(korean) { }
        public void SetAs(string subject)
        {
            if (LangMedia.IsSameWith(subject))
                Selected = LangMedia;
            else if (SpeechWriting.IsSameWith(subject))
                Selected = SpeechWriting;
        }

        public override string FullName => Selected.FullName;
        public override string? ShortName => Selected.ShortName;
        public override string Name => this.ShortName ?? this.FullName;
    }


    public class Math : Subject, ISelectiveSubject
    {
        public static Subject Probability => new("확률과 통계", "확통");
        public static Subject Daic => new("미적분");
        public static Subject Default => new("수학");
        public static Subject Selected { get; set; } = Default;
        public Math() : base(Default) { }
        public Math(in Subject math) : base(math) { }
        public void SetAs(string subject)
        {
            if (Probability.IsSameWith(subject))
                Selected = Probability;
            else if (Daic.IsSameWith(subject))
                Selected = Daic;
        }

        public override string FullName => Selected.FullName;
        public override string? ShortName => Selected.ShortName;
        public override string Name => this.ShortName ?? this.FullName;
    }

    public class Social : Subject, ISelectiveSubject
    {
        public static Subject Eastern => new("동아시아사", "동사");
        public static Subject KoreanGeo => new("한국지리");
        public static Subject Culture => new("사회·문화");
        public static Subject Default => new("사회");
        public static Subject Selected { get; set; } = Default;
        public Social() : base(Default) { }
        public Social(in Subject social) : base(social) { }
        public void SetAs(string subject)
        {
            if (Eastern.IsSameWith(subject))
                Selected = Eastern;
            else if (KoreanGeo.IsSameWith(subject))
                Selected = KoreanGeo;
            else if (Culture.IsSameWith(subject))
                Selected= Culture;
        }

        public override string FullName => Selected.FullName;
        public override string? ShortName => Selected.ShortName;
        public override string Name => this.ShortName ?? this.FullName;
    }

    public class Language : Subject, ISelectiveSubject
    {
        public static Subject Spanish => new("스페인어권 문화", "스문");
        public static Subject Japanese => new("일본문화");
        public static Subject Chinese => new("중국문화");
        public static Subject Default => new("외국어");
        public static Subject Selected { get; set; } = Default;
        public Language() : base(Default) { }
        public Language(in Subject language) : base(language) { }
        public void SetAs(string subject)
        {
            if (Spanish.IsSameWith(subject))
                Selected = Spanish;
            else if (Japanese.IsSameWith(subject))
                Selected = Japanese;
            else if (Chinese.IsSameWith(subject))
                Selected = Chinese;
        }

        public override string FullName => Selected.FullName;
        public override string? ShortName => Selected.ShortName;
        public override string Name => this.ShortName ?? this.FullName;
    }

    public class Global1 : Subject, ISelectiveSubject
    {
        public static Subject SocialResearch => new("사회 탐구 방법", "사탐방");
        public static Subject KoreanSociety => new("한국 사회의 이해", "한사이");
        public static Subject Default => new("국제1");
        public static Subject Selected { get; set; } = Default;
        public Global1() : base(Default) { }
        public Global1(in Subject global1) : base(global1) { }
        public void SetAs(string subject)
        {
            if (SocialResearch.IsSameWith(subject))
                Selected = SocialResearch;
            else if (KoreanSociety.IsSameWith(subject))
                Selected = KoreanSociety;
        }

        public override string FullName => Selected.FullName;
        public override string? ShortName => Selected.ShortName;
        public override string Name => this.ShortName ?? this.FullName;
    }

    public class Global2 : Subject, ISelectiveSubject
    {
        public static Subject FutureSociety => new("세계 문제와 미래 사회", "세문미");
        public static Subject Ethics => new("윤리학 연습", "윤연");
        public static Subject Default => new("국제2");
        public static Subject Selected { get; set; } = Default;
        public Global2() : base(Default) { }
        public Global2(in Subject global2) : base(global2) { }
        public void SetAs(string subject)
        {
            if (FutureSociety.IsSameWith(Selected))
                Selected = FutureSociety;
            else if (Ethics.IsSameWith(subject))
                Selected = Ethics;
        }

        public override string FullName => Selected.FullName;
        public override string? ShortName => Selected.ShortName;
        public override string Name => this.ShortName ?? this.FullName;
    }

    namespace Grade3.Semester1
    {
        public static class Subjects
        {
            public static Korean Korean { get; set; } = new();
            public static Math Math { get; set; } = new();
            public static Social Social { get; set; } = new();
            public static Language Language { get; set; } = new();
            public static Global1 Global1 { get; set; } = new();
            public static Global2 Global2 { get; set; } = new();

            public static Subject EnglishLiterature => new("영미 문학 읽기", "영문");
            public static Subject Sports => new("체육");
            public static Subject Reading => new("독서와 의사소통", "독의");
            public static Subject AdvancedEnglish => new("심화영어Ⅱ", "심영Ⅱ");

            public static Subject HomeComing => new("홈커밍");
            public static Subject Others => new("창의적 체험활동", "창체");

            public static void ResetSelectiveSubjects()
            {
                Korean = new(Korean.Default);
                Math = new(Math.Default);
                Social = new(Social.Default);
                Language = new(Language.Default);
                Global1 = new(Global1.Default);
                Global2 = new(Global2.Default);
            }
        }
    }
}