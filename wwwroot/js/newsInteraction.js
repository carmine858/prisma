/**
 * Prisma News - Interactive News Components
 */
document.addEventListener('DOMContentLoaded', function () {
    // Category Navigation Scrolling
    initCategoryNavigation();

    // Category Switching
    initCategorySwitching();

    // Animation on scroll
    initScrollAnimation();

    // Card hover effect
    initCardHoverEffect();
});

/**
 * Initialize horizontal scrolling for category navigation
 */
function initCategoryNavigation() {
    const categoryNav = document.querySelector('.category-nav');
    const prevArrow = document.querySelector('.prev-arrow');
    const nextArrow = document.querySelector('.next-arrow');

    if (!categoryNav || !prevArrow || !nextArrow) return;

    // Scroll by clicking arrows
    nextArrow.addEventListener('click', () => {
        categoryNav.scrollBy({
            left: 200,
            behavior: 'smooth'
        });
    });

    prevArrow.addEventListener('click', () => {
        categoryNav.scrollBy({
            left: -200,
            behavior: 'smooth'
        });
    });

    // Check if arrows should be visible
    function updateArrowsVisibility() {
        const isScrollable = categoryNav.scrollWidth > categoryNav.clientWidth;
        const isAtStart = categoryNav.scrollLeft <= 10;
        const isAtEnd = categoryNav.scrollLeft + categoryNav.clientWidth >= categoryNav.scrollWidth - 10;

        prevArrow.style.opacity = isScrollable && !isAtStart ? '1' : '0.3';
        nextArrow.style.opacity = isScrollable && !isAtEnd ? '1' : '0.3';
    }

    categoryNav.addEventListener('scroll', updateArrowsVisibility);
    window.addEventListener('resize', updateArrowsVisibility);
    updateArrowsVisibility();
}

/**
 * Initialize category switching functionality
 */
function initCategorySwitching() {
    const categoryLinks = document.querySelectorAll('.category-nav-link');
    const expandButtons = document.querySelectorAll('.category-expander, .category-expand-btn');

    // Category tab navigation
    categoryLinks.forEach(link => {
        link.addEventListener('click', (e) => {
            e.preventDefault();

            // Update active link
            categoryLinks.forEach(l => l.classList.remove('active'));
            link.classList.add('active');

            // Show corresponding panel
            const targetCategory = link.getAttribute('data-category');
            const panels = document.querySelectorAll('.category-panel');

            panels.forEach(panel => {
                panel.classList.remove('active');
            });

            const targetPanel = document.getElementById(`category-${targetCategory}`);
            if (targetPanel) {
                targetPanel.classList.add('active');

                // Add fade in animation
                targetPanel.style.animation = 'none';
                setTimeout(() => {
                    targetPanel.style.animation = 'fadeIn 0.3s ease-in-out';
                }, 10);
            }

            // Scroll to top of the section
            const categoriesSection = document.querySelector('.categories-section');
            if (categoriesSection) {
                window.scrollTo({
                    top: categoriesSection.offsetTop - 100,
                    behavior: 'smooth'
                });
            }
        });
    });

    // Expand buttons to switch to category detail view
    expandButtons.forEach(btn => {
        btn.addEventListener('click', () => {
            const categorySlug = btn.getAttribute('data-category');
            const categoryLink = document.querySelector(`.category-nav-link[data-category="${categorySlug}"]`);

            if (categoryLink) {
                categoryLink.click();
            }
        });
    });
}

/**
 * Initialize animation on scroll
 */
function initScrollAnimation() {
    const animateElements = document.querySelectorAll('.animate__animated');

    if (!animateElements.length) return;

    const observer = new IntersectionObserver((entries) => {
        entries.forEach(entry => {
            if (entry.isIntersecting) {
                const animation = entry.target.getAttribute('data-animation');
                const delay = entry.target.getAttribute('data-delay');

                if (animation) {
                    entry.target.classList.add(animation);

                    if (delay) {
                        entry.target.style.animationDelay = `${delay}ms`;
                    }
                }

                observer.unobserve(entry.target);
            }
        });
    }, { threshold: 0.1 });

    animateElements.forEach(element => {
        observer.observe(element);
    });
}

/**
 * Add subtle hover effects to cards
 */
function initCardHoverEffect() {
    const cards = document.querySelectorAll('.news-card, .trending-card-xl, .trending-card-horizontal, .trending-card-small, .category-main-article');

    if (!cards.length) return;

    cards.forEach(card => {
        card.addEventListener('mouseenter', function () {
            // Add subtle lift and shadow effect
            this.style.transform = 'translateY(-5px)';
            this.style.boxShadow = '0 15px 30px rgba(0,0,0,0.1)';

            // Add subtle scale to image
            const image = this.querySelector('img');
            if (image) {
                image.style.transform = 'scale(1.05)';
            }
        });

        card.addEventListener('mouseleave', function () {
            // Reset transform and shadow
            this.style.transform = '';
            this.style.boxShadow = '';

            // Reset image scale
            const image = this.querySelector('img');
            if (image) {
                image.style.transform = '';
            }
        });
    });
}