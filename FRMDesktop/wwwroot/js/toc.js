const toc = {};
toc.init = () => {
    render();
    position();

    function render() {
        const headings = document.querySelectorAll(".info-article-layout-content h2");
        const html = Array.from(headings)
            .map((h2) => `<li><a href='#${h2.id}'>${h2.innerText}</a></li>`)
            .join("");

        const element = document.querySelector(".info-article-layout-toc");

        if (element != null) element.innerHTML = html;
    }

    function position() {
        const main = document.querySelector(".info-article-layout");
        const sidebar = document.querySelector("sidebar");

        if (main != null && sidebar != null) {
            function update() {
                sidebar.classList.toggle("info-article-layout-sidebar-fixed", window.scrollY > main.offsetTop);
            }
            window.addEventListener("scroll", update);
            update();
        }
    }
};
