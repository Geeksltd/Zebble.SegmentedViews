﻿namespace Zebble
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Olive;

    public partial class SegmentedViews : Stack
    {
        public TimeSpan AnimationDuration = 300.Milliseconds();
        public AnimationEasing Easing = AnimationEasing.EaseInOut;
        public bool IsFirstSegmentActive = true;
        public AsyncEvent<Segment> SelectedSegmentChanged = new AsyncEvent<Segment>();

        public SegmentedViews() { ClipChildren = false; }
    }

    public class Segments : Stack
    {
        public Segment[] Items => this.CurrentChildren<Segment>().ToArray();
        public Segments() : base(RepeatDirection.Horizontal) { Id = "SegmentsContainer"; }
    }

    public class Segment : Stack
    {
        public int? Index => Container?.CurrentChildren<Segment>()?.IndexOf(this);
        public Segments Container => FindParent<Segments>();
        public SegmentedViews RootView => Container.FindParent<SegmentedViews>();

        public override async Task OnInitializing()
        {
            await base.OnInitializing();
            Tapped.Handle(HandleSegmentTap);
        }

        async Task HandleSegmentTap()
        {
            if (Container == null || Index == null)
            {
                Log.For(this).Error("Each Segment should be inside a Segments view.");
                return;
            }

            if (Root == null)
            {
                Log.For(this).Error("Segments should be inside a SegmentedViews view.");
                return;
            }

            Container.Items.Do(async (b) => await b.UnsetPseudoCssState("active"));
            await SetPseudoCssState("active");

            var contents = Container.CurrentSiblings<Contents>().FirstOrDefault();

            if (contents == null)
            {
                Log.For(this).Error("Each content should be inside a Contents view.");
                return;
            }

            var content = contents.CurrentChildren<Content>()?.ToArray()[Index.Value];

            if (content == null)
            {
                Log.For(this).Error("Each content should be inside a Contents view.");
                return;
            }

            await contents.Animate(
                RootView.AnimationDuration,
                RootView.Easing,
                (c) => c.X(-c.Items[Index.Value].X.CurrentValue));

            if (!content.IsActuallyShown)
            {
                content.IsActuallyShown = true;
                await content.RaiseShown();
            }

            if (content.NavigatedTo != null)
            {
                await Task.Delay(50);
                await content.NavigatedTo?.Raise();
            }

            await RootView.SelectedSegmentChanged.Raise(this);
        }
    }

    public class Contents : Stack
    {
        public Content[] Items => this.CurrentChildren<Content>().ToArray();
        public Contents() : base(RepeatDirection.Horizontal) { Id = "ViewsContainer"; ClipChildren = false; }
    }

    public class Content : LazyLoader
    {
        public AsyncEvent NavigatedTo = new AsyncEvent();

        public bool IsActuallyShown { get; set; }

        public int? Index => Container?.CurrentChildren<Content>()?.IndexOf(this);

        public Contents Container => FindParent<Contents>();

        public async Task RaiseShown()
        {
            if (!IsActuallyShown && this != Container?.CurrentChildren<Content>()?.FirstOrDefault()) return;
            await Loaded();
            Container.Width(Container.CurrentChildren.Sum(x => x.ActualWidth));
        }
    }
}
