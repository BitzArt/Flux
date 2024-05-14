function getInnerText(id) {
    let element = document.getElementById(id);
    if (element) {
        return element.innerText;
    }
    return null;
}