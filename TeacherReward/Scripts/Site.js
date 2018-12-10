function show(nextrowID, thirdrowID, fourthrowID) {
    var nextrow = document.getElementById(nextrowID);
    var thirdrow = document.getElementById(thirdrowID);
    var fourthrow = document.getElementById(fourthrowID);

    if (nextrow.style.display == "none") {
        nextrow.style.display = "";
    } else if (thirdrow.style.display == "none") {
        thirdrow.style.display = "";
    } else if (fourthrow.style.display == "none") {
        fourthrow.style.display = "";
    } else {
        alert("最多支持四项内容");
    }
}

function hidePrize(thisrowID, textbox1ID, ddlist1ID, ddlist2ID, textbox2ID) {
    var thisrow = document.getElementById(thisrowID);
    var textbox1 = document.getElementById(textbox1ID);
    var textbox2 = document.getElementById(textbox2ID);
    var ddlist1 = document.getElementById(ddlist1ID);
    var ddlist2 = document.getElementById(ddlist2ID);

    thisrow.style.display = "none";
    textbox1.value = "";
    textbox2.value = "";
    ddlist1.selectedIndex = 0;
    ddlist2.selectedIndex = 0;
}

function hideProject(thisrowID, textbox1ID, ddlist1ID, textbox2ID, textbox3ID, ddlist2ID, textbox4ID) {
    var thisrow = document.getElementById(thisrowID);
    var textbox1 = document.getElementById(textbox1ID);
    var textbox2 = document.getElementById(textbox2ID);
    var textbox3 = document.getElementById(textbox3ID);
    var textbox4 = document.getElementById(textbox4ID);
    var ddlist1 = document.getElementById(ddlist1ID);
    var ddlist2 = document.getElementById(ddlist2ID);

    thisrow.style.display = "none";
    textbox1.value = "";
    textbox2.value = "";
    textbox3.value = "";
    textbox4.value = "";
    ddlist1.selectedIndex = 0;
    ddlist2.selectedIndex = 0;
}

function hideTT(thisrowID, textbox1ID, textbox2ID, ddlist1ID, ddlist2ID, textbox3ID) {
    var thisrow = document.getElementById(thisrowID);
    var textbox1 = document.getElementById(textbox1ID);
    var textbox2 = document.getElementById(textbox2ID);
    var textbox3 = document.getElementById(textbox3ID);
    var ddlist1 = document.getElementById(ddlist1ID);
    var ddlist2 = document.getElementById(ddlist2ID);

    thisrow.style.display = "none";
    textbox1.value = "";
    textbox2.value = "";
    textbox3.value = "";
    ddlist1.selectedIndex = 0;
    ddlist2.selectedIndex = 0;
}

function hidePT(thisrowID, textbox1ID, ddlist1ID, ddlist2ID, textbox2ID, textbox3ID) {
    var thisrow = document.getElementById(thisrowID);
    var textbox1 = document.getElementById(textbox1ID);
    var textbox2 = document.getElementById(textbox2ID);
    var textbox3 = document.getElementById(textbox3ID);
    var ddlist1 = document.getElementById(ddlist1ID);
    var ddlist2 = document.getElementById(ddlist2ID);

    thisrow.style.display = "none";
    textbox1.value = "";
    textbox2.value = "";
    textbox3.value = "";
    ddlist1.selectedIndex = 0;
    ddlist2.selectedIndex = 0;
}

function hideT(thisrowID, textbox1ID, ddlist1ID, textbox2ID, textbox3ID, ddlist2ID, textbox4ID) {
    var thisrow = document.getElementById(thisrowID);
    var textbox1 = document.getElementById(textbox1ID);
    var textbox2 = document.getElementById(textbox2ID);
    var textbox3 = document.getElementById(textbox3ID);
    var textbox4 = document.getElementById(textbox4ID);
    var ddlist1 = document.getElementById(ddlist1ID);
    var ddlist2 = document.getElementById(ddlist2ID);

    thisrow.style.display = "none";
    textbox1.value = "";
    textbox2.value = "";
    textbox3.value = "";
    textbox4.value = "";
    ddlist1.selectedIndex = 0;
    ddlist2.selectedIndex = 0;
}

function hideCT(thisrowID, textbox1ID, ddlist1ID, textbox2ID, textbox3ID, textbox4ID) {
    var thisrow = document.getElementById(thisrowID);
    var textbox1 = document.getElementById(textbox1ID);
    var textbox2 = document.getElementById(textbox2ID);
    var textbox3 = document.getElementById(textbox3ID);
    var textbox4 = document.getElementById(textbox4ID);
    var ddlist1 = document.getElementById(ddlist1ID);

    thisrow.style.display = "none";
    textbox1.value = "";
    textbox2.value = "";
    textbox3.value = "";
    textbox4.value = "";
    ddlist1.selectedIndex = 0;
}