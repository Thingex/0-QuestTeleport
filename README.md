# 0-QuestTeleport
<p>As title illustrates, the module adds a function that can teleport to the POI of one's quest. Several settings are provided publically for players to customize their module, feel free to modify them in class "Customization", each item with detailed description attached. If you excel in CSharp or mod developing, you can even create a visible file for a more convenient modification.</p>

And...Before you experience, you'd make sure that the two following files are prepared correctly.

<b>windows.xml (in .../XUi)</b>
~~~
<thingex>
    <insertAfter xpath="//window[@name='windowQuestList']/rect[@name='content']/rect[1]/button[last()]">
        <button depth="1" 
        name="btnTeleToQuest" 
        style="icon32px, press, hover" 
        pivot="center" pos="198,-21" 
        sprite="ui_game_symbol_twitch_jump" 
        tooltip="即刻传送" 
        tooltip_key="即刻传送" 
        sound="[paging_click]" />
    </insertAfter>
    <set xpath="//window[@name='windowQuestList']/rect[@name='content']/rect[1]/sprite[@name='searchIcon']/@pos">240,-20</set>
    <set xpath="//window[@name='windowQuestList']/rect[@name='content']/rect[1]/panel[1]/textfield/@width">80</set>
    <set xpath="//window[@name='windowQuestList']/rect[@name='content']/rect[1]/panel[1]/textfield/@pos">30,0</set>
</thingex>
~~~
<b>ModInfo.xml (in ...)</b>
~~~
<?xml version="1.0" encoding="UTF-8" ?>
<xml>
	<Name value="0-QuestTeleport" />
	<DisplayName value="0-QuestTeleport" />
	<Description value="I cant find a word to desc it, LoL:)" />
	<Author value="东方清华EX" />
	<Version value="2.0" />
	<Website value="" />
	<SkipWithAntiCheat value="true" />
</xml>
~~~
