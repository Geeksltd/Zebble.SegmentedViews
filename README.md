[logo]: https://raw.githubusercontent.com/Geeksltd/Zebble.SegmentedViews/master/Shared/NuGet/Icon.png "Zebble.SegmentedViews"


## Zebble.SegmentedViews

![logo]

A Zebble plugin that allow you to devide a page into multiple page.


[![NuGet](https://img.shields.io/nuget/v/Zebble.SegmentedViews.svg?label=NuGet)](https://www.nuget.org/packages/Zebble.SegmentedViews/)

> A SegmentedView plugin is a horizontal aligned set of segment (Views), containing unlimited buttons, that act as tab pages.

<br>


### Setup
* Available on NuGet: [https://www.nuget.org/packages/Zebble.SegmentedViews/](https://www.nuget.org/packages/Zebble.SegmentedViews/)
* Install in your platform client projects.
* Available for iOS, Android and UWP.
<br>


### Api Usage
 This plugin has a different views that allow you to design your segmented view like a tab in a page and each page can contain unlimited segmented views.

##### Segments:

This view keeps all of the button that show you which content is enabled and user can changed the content by using them and it define like below:
```xml
<SegmentedViews>
    <Segments>
      <Segment Id="PendingSegment" Text="Pending" PseudoCssState="active"></Segment>
      <Segment Id="KnownSegment" Text="Known"></Segment>
      <Segment Id="ToLearnSegment" Text="To Learn"></Segment>
    </Segments>
    ...
  </SegmentedViews>
```

##### Contents:

This view keeps all of the contents of each segment and they can contains any view of Zebble that user need like below:
```xml
<SegmentedViews>
    ...
    <Contents>
      <Content Id="PendingContent">
        <!-- content goes here -->
      </Content>
      <Content Id="KnownContent">
        <ScrollView>
          <!-- content goes here -->
        </ScrollView>
      </Content>
      <Content Id="ToLearnContent">
        <ScrollView>
          <!-- content goes here -->
        </ScrollView>
      </Content>
        ...
    </Contents>
  </SegmentedViews>
```
##### SegmentedViews
You can use this plugin like below code:
```xml
<SegmentedViews>
    <Segments>
      <Segment Id="PendingSegment" Text="Pending" PseudoCssState="active"></Segment>
      <Segment Id="KnownSegment" Text="Known"></Segment>
      <Segment Id="ToLearnSegment" Text="To Learn"></Segment>
    </Segments>
    <Contents>
      <Content Id="PendingContent">
        <ScrollView>
          <Modules.PendingList />
        </ScrollView>
      </Content>
      <Content Id="KnownContent">
        <ScrollView>
          <Modules.KnownList/>
        </ScrollView>
      </Content>
      <Content Id="ToLearnContent">
        <ScrollView>
          <Modules.ToLearnList/>
        </ScrollView>
      </Content>
    </Contents>
  </SegmentedViews>
```

### Properties
| Property     | Type         | Android | iOS | Windows |
| :----------- | :----------- | :------ | :-- | :------ |
| AnimationDuration            | TimeSpan           | x       | x   | x       |
| Easing            | AnimationEasing           | x       | x   | x       |
| IsFirstSegmentActive            | bool           | x       | x   | x       |
