window.onload = async function afterWebPageLoad() {
    await new Promise(r => setTimeout(r, 500));
    const handleOnMouseMove = e => {
        const { currentTarget: target } = e;

        const rect = target.getBoundingClientRect(),
            x = e.clientX - rect.left,
            y = e.clientY - rect.top;

        target.style.setProperty("--mouse-x", `${x}px`);
        target.style.setProperty("--mouse-y", `${y}px`);
    }

for (const user_card of document.querySelectorAll(".user_card")) {
    user_card.onmousemove = e => handleOnMouseMove(e);
}
}