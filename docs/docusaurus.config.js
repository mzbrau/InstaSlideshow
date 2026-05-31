// @ts-check
// `@type` JSDoc annotations allow editor autocompletion and type checking
// (when paired with `@ts-check`).
// There are various equivalent ways to declare your Docusaurus config.
// See: https://docusaurus.io/docs/api/docusaurus-config

import { themes as prismThemes } from 'prism-react-renderer';

/** @type {import('@docusaurus/types').Config} */
const config = {
  title: 'InstaSlideshow',
  tagline: 'Display Instagram hashtag photos on any screen — beautifully.',
  favicon: 'img/logo.svg',

  // Set the production url of your site here
  url: 'https://mzbrau.github.io',
  // Set the /<baseUrl>/ pathname under which your site is served
  baseUrl: '/InstaSlideshow/',

  // GitHub pages deployment config.
  organizationName: 'mzbrau',
  projectName: 'InstaSlideshow',
  trailingSlash: false,

  onBrokenLinks: 'throw',
  onBrokenMarkdownLinks: 'warn',

  i18n: {
    defaultLocale: 'en',
    locales: ['en'],
  },

  presets: [
    [
      'classic',
      /** @type {import('@docusaurus/preset-classic').Options} */
      ({
        docs: {
          sidebarPath: './sidebars.js',
          editUrl: 'https://github.com/mzbrau/InstaSlideshow/tree/main/docs/',
        },
        blog: false,
        theme: {
          customCss: './src/css/custom.css',
        },
      }),
    ],
  ],

  themeConfig:
    /** @type {import('@docusaurus/preset-classic').ThemeConfig} */
    ({
      image: 'img/instaslideshow-social.svg',
      navbar: {
        title: 'InstaSlideshow',
        logo: {
          alt: 'InstaSlideshow Logo',
          src: 'img/logo.svg',
        },
        items: [
          {
            type: 'docSidebar',
            sidebarId: 'tutorialSidebar',
            position: 'left',
            label: 'Docs',
          },
          {
            href: 'https://github.com/mzbrau/InstaSlideshow',
            label: 'GitHub',
            position: 'right',
          },
        ],
      },
      footer: {
        style: 'dark',
        links: [
          {
            title: 'Docs',
            items: [
              { label: 'Introduction', to: '/docs/intro' },
              { label: 'Getting Started', to: '/docs/getting-started' },
              { label: 'Configuration', to: '/docs/configuration' },
            ],
          },
          {
            title: 'More',
            items: [
              {
                label: 'GitHub',
                href: 'https://github.com/mzbrau/InstaSlideshow',
              },
              {
                label: 'Report an Issue',
                href: 'https://github.com/mzbrau/InstaSlideshow/issues',
              },
            ],
          },
        ],
        copyright: `Copyright © ${new Date().getFullYear()} InstaSlideshow. Built with Docusaurus.`,
      },
      prism: {
        theme: prismThemes.github,
        darkTheme: prismThemes.dracula,
        additionalLanguages: ['csharp', 'xml', 'bash', 'powershell'],
      },
      colorMode: {
        defaultMode: 'dark',
        disableSwitch: false,
        respectPrefersColorScheme: true,
      },
    }),
};

export default config;
