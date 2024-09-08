# 0-QuestTeleport
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
