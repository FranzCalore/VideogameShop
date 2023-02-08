window.onload = async function afterWebPageLoad() {
    await new Promise(r => setTimeout(r, 500));

    document.getElementById("user_cards").onmousemove = e => {
        for (const card of document.getElementsByClassName("user_card")) {
            const rect = card.getBoundingClientRect(),
                x = e.clientX - rect.left,
                y = e.clientY - rect.top;

                card.style.setProperty("--mouse-x", `${x}px`);
                card.style.setProperty("--mouse-y", `${y}px`);
        }
    }
}