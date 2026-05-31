import React from 'react';
import clsx from 'clsx';
import Link from '@docusaurus/Link';
import useDocusaurusContext from '@docusaurus/useDocusaurusContext';
import Layout from '@theme/Layout';

import styles from './index.module.css';

const features = [
  {
    icon: '📸',
    title: 'Hashtag-Powered',
    description:
      'Point InstaSlideshow at any Instagram hashtag and watch live public posts flow across your display in real time.',
  },
  {
    icon: '🎨',
    title: 'Full-Screen WPF',
    description:
      'A borderless, full-screen WPF window with smooth cross-fade transitions delivers a polished, kiosk-ready experience.',
  },
  {
    icon: '⚙️',
    title: 'Zero Code Config',
    description:
      'All settings — hashtag, credentials, transition speed, date filters — live in a single App.config file. No recompile needed.',
  },
  {
    icon: '🔄',
    title: 'Auto-Refresh',
    description:
      'A configurable timer continuously fetches fresh pages of media so your display never shows stale content.',
  },
  {
    icon: '📋',
    title: 'Carousel Support',
    description:
      'Multi-image carousel posts are fully supported — every photo in an album gets its turn on screen.',
  },
  {
    icon: '🛡️',
    title: 'Date Filtering',
    description:
      'Set a start date to show only photos from your event, keeping irrelevant older content off the screen.',
  },
];

function FeatureCard({ icon, title, description }) {
  return (
    <div className={clsx('col col--4', styles.featureCol)}>
      <div className="feature-card">
        <span className="feature-card__icon">{icon}</span>
        <p className="feature-card__title">{title}</p>
        <p>{description}</p>
      </div>
    </div>
  );
}

function HeroBanner() {
  const { siteConfig } = useDocusaurusContext();
  return (
    <header className={clsx('hero hero--insta', styles.heroBanner)}>
      <div className="container">
        <div className={styles.heroLogo}>📷</div>
        <h1 className="hero__title">{siteConfig.title}</h1>
        <p className="hero__subtitle">{siteConfig.tagline}</p>

        <div className={styles.heroButtons}>
          <Link
            className="button button--lg button--insta-primary"
            to="/docs/getting-started"
          >
            🚀 Get Started
          </Link>
          <Link
            className="button button--lg button--insta-outline"
            to="/docs/intro"
          >
            Learn More
          </Link>
        </div>

        <div className="badge-strip">
          <img
            alt=".NET Framework"
            src="https://img.shields.io/badge/.NET_Framework-4.6.1-purple?style=flat-square"
          />
          <img
            alt="WPF"
            src="https://img.shields.io/badge/UI-WPF-c13584?style=flat-square"
          />
          <img
            alt="C#"
            src="https://img.shields.io/badge/language-C%23-blueviolet?style=flat-square"
          />
          <img
            alt="License"
            src="https://img.shields.io/github/license/mzbrau/InstaSlideshow?style=flat-square&color=e1306c"
          />
        </div>
      </div>
    </header>
  );
}

export default function Home() {
  return (
    <Layout
      title="InstaSlideshow — Instagram Hashtag Slideshow"
      description="Display live Instagram hashtag photos in a full-screen WPF slideshow. Zero code config, carousel support, and automatic refresh."
    >
      <HeroBanner />

      <main>
        {/* Features grid */}
        <section className="features">
          <div className="container">
            <h2 className={styles.sectionHeading}>Why InstaSlideshow?</h2>
            <div className="row">
              {features.map((props, idx) => (
                <FeatureCard key={idx} {...props} />
              ))}
            </div>
          </div>
        </section>

        {/* Quick-start strip */}
        <section className={styles.quickStart}>
          <div className="container">
            <h2 className={styles.sectionHeading}>Up and running in minutes</h2>
            <div className="row">
              <div className={clsx('col col--4', styles.stepCol)}>
                <div className={styles.stepNumber}>1</div>
                <h3>Install</h3>
                <p>
                  Run the Visual Studio installer or grab the latest release from
                  GitHub.
                </p>
              </div>
              <div className={clsx('col col--4', styles.stepCol)}>
                <div className={styles.stepNumber}>2</div>
                <h3>Configure</h3>
                <p>
                  Open <code>App.config</code> and fill in your Instagram
                  credentials and the target hashtag.
                </p>
              </div>
              <div className={clsx('col col--4', styles.stepCol)}>
                <div className={styles.stepNumber}>3</div>
                <h3>Run</h3>
                <p>
                  Launch the application (as Administrator if installed under{' '}
                  <code>C:\Program Files\</code>) and enjoy a live, full-screen
                  Instagram hashtag slideshow.
                </p>
              </div>
            </div>
            <div className={styles.quickStartCta}>
              <Link
                className="button button--lg button--insta-primary"
                to="/docs/getting-started"
              >
                Read the full guide →
              </Link>
            </div>
          </div>
        </section>
      </main>
    </Layout>
  );
}
