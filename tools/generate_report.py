from datetime import date
from pathlib import Path

from reportlab.lib import colors
from reportlab.lib.pagesizes import A4
from reportlab.lib.styles import ParagraphStyle, getSampleStyleSheet
from reportlab.lib.units import cm
from reportlab.platypus import (
    PageBreak,
    Paragraph,
    SimpleDocTemplate,
    Spacer,
    Table,
    TableStyle,
)


ROOT = Path(__file__).resolve().parents[1]
OUTPUT = ROOT / "deliverables" / "TestingReport.pdf"
REPOSITORY_URL = "https://github.com/fa1dpip/word-frequency-analyzer-csharp"


def add_title_page(story, styles):
    story.append(Spacer(1, 3.0 * cm))
    story.append(Paragraph("Testing Report", styles["Title"]))
    story.append(Spacer(1, 0.4 * cm))
    story.append(Paragraph("Word Frequency Analyzer C# CLI", styles["Subtitle"]))
    story.append(Spacer(1, 1.2 * cm))
    story.append(Paragraph("<b>Repository:</b>", styles["Body"]))
    story.append(Paragraph(REPOSITORY_URL, styles["Link"]))
    story.append(Spacer(1, 0.7 * cm))
    story.append(Paragraph("<b>Date:</b> " + date.today().strftime("%B %d, %Y"), styles["Body"]))
    story.append(Spacer(1, 0.7 * cm))
    story.append(
        Paragraph(
            "The project contains two code versions: Version 1 for basic word counting "
            "and Version 2 with configurable ending trimming using parameters N and M.",
            styles["Body"],
        )
    )
    story.append(PageBreak())


def add_overview(story, styles):
    story.append(Paragraph("1. Objective", styles["Heading1"]))
    story.append(
        Paragraph(
            "The application reads all .txt files from a selected directory, loads their "
            "contents, splits text by whitespace and punctuation, converts words to lowercase, "
            "counts frequencies with Dictionary&lt;string, int&gt;, and prints the result.",
            styles["Body"],
        )
    )
    story.append(Spacer(1, 0.3 * cm))
    story.append(
        Paragraph(
            "Version 2 extends this behavior: if a word length is greater than N, the last M "
            "characters are removed before the word is counted.",
            styles["Body"],
        )
    )
    story.append(Spacer(1, 0.5 * cm))

    story.append(Paragraph("2. OOP Structure", styles["Heading1"]))
    data = [
        ["Class", "Responsibility"],
        ["FileReader", "Validates a directory and reads UTF-8 text from .txt files."],
        ["TextProcessor", "Splits text into lowercase words and ignores empty entries."],
        ["WordCounter", "Stores and updates word frequencies using Dictionary<string, int>."],
        ["EndingTrimmer", "Version 2 class that applies the N/M ending-removal rule."],
        ["ResultPrinter", "Prints sorted word-frequency results to the console."],
        ["Program", "Parses CLI arguments and coordinates the workflow."],
    ]
    story.append(make_table(data, [4.0 * cm, 12.0 * cm]))
    story.append(Spacer(1, 0.5 * cm))

    story.append(Paragraph("3. Test Environment", styles["Heading1"]))
    env_data = [
        ["Item", "Value"],
        ["Operating system", "Windows"],
        ["Compiler", ".NET Framework C# compiler 4.8, C# 5"],
        ["Build output", "compiled/Version1/Debug and compiled/Version2/Debug"],
        ["Test command", "powershell -ExecutionPolicy Bypass -File .\\tests\\run-tests.ps1"],
    ]
    story.append(make_table(env_data, [4.5 * cm, 11.5 * cm]))
    story.append(Spacer(1, 0.5 * cm))


def add_tests(story, styles):
    story.append(Paragraph("4. Test Cases", styles["Heading1"]))
    data = [
        ["No.", "Scenario", "Input", "Expected result", "Status"],
        [
            "1",
            "Version 1 basic counting",
            "Two small .txt files with mixed case and punctuation",
            "hello=3, world=3, car=2, cars=1, engine=2",
            "Passed",
        ],
        [
            "2",
            "Version 2 ending trimming",
            "Same files, N=3, M=1",
            "cars, car, CAR counted together as car=3; hello becomes hell=3",
            "Passed",
        ],
        [
            "3",
            "Version 2 threshold check",
            "Same files, N=6, M=2",
            "No sample word longer than 6 is changed; result matches Version 1",
            "Passed",
        ],
        [
            "4",
            "Build verification",
            "Build script compiles both source folders",
            "Two Debug folders are produced with executable and PDB files",
            "Passed",
        ],
    ]
    story.append(make_table(data, [1.0 * cm, 4.0 * cm, 4.2 * cm, 5.4 * cm, 1.4 * cm]))
    story.append(Spacer(1, 0.5 * cm))

    story.append(Paragraph("5. Console Output Samples", styles["Heading1"]))
    story.append(Paragraph("<b>Version 1:</b>", styles["Body"]))
    story.append(
        Paragraph(
            "<font name='Courier'>hello 3<br/>world 3<br/>car 2<br/>engine 2<br/>cars 1</font>",
            styles["Body"],
        )
    )
    story.append(Spacer(1, 0.25 * cm))
    story.append(Paragraph("<b>Version 2 with N=3 and M=1:</b>", styles["Body"]))
    story.append(
        Paragraph(
            "<font name='Courier'>car 3<br/>hell 3<br/>worl 3<br/>engin 2</font>",
            styles["Body"],
        )
    )
    story.append(Spacer(1, 0.5 * cm))

    story.append(Paragraph("6. Deliverables", styles["Heading1"]))
    deliverables = [
        "Source code in src/Version1 and src/Version2.",
        "deliverables/source-code.zip with the source folders, build script, tests, and README.",
        "Git tags version-1 and version-2 for both code versions.",
        "deliverables/txt-file-examples.zip with four different .txt files, each larger than 1 MB.",
        "deliverables/WordFrequencyAnalyzerV1_Debug.zip and deliverables/WordFrequencyAnalyzerV2_Debug.zip.",
        "deliverables/TestingReport.pdf.",
    ]
    for item in deliverables:
        story.append(Paragraph("- " + item, styles["Body"]))
    story.append(Spacer(1, 0.5 * cm))

    story.append(Paragraph("7. Conclusion", styles["Heading1"]))
    story.append(
        Paragraph(
            "All planned tests passed. The application meets the requirements for case-insensitive "
            "counting, punctuation handling, Dictionary-based frequency storage, and OOP separation "
            "of responsibilities.",
            styles["Body"],
        )
    )


def make_table(data, widths):
    table = Table(data, colWidths=widths, repeatRows=1)
    table.setStyle(
        TableStyle(
            [
                ("BACKGROUND", (0, 0), (-1, 0), colors.HexColor("#E6EAF2")),
                ("TEXTCOLOR", (0, 0), (-1, 0), colors.HexColor("#1D2433")),
                ("GRID", (0, 0), (-1, -1), 0.5, colors.HexColor("#AAB2C0")),
                ("FONTNAME", (0, 0), (-1, 0), "Helvetica-Bold"),
                ("VALIGN", (0, 0), (-1, -1), "TOP"),
                ("LEFTPADDING", (0, 0), (-1, -1), 6),
                ("RIGHTPADDING", (0, 0), (-1, -1), 6),
                ("TOPPADDING", (0, 0), (-1, -1), 5),
                ("BOTTOMPADDING", (0, 0), (-1, -1), 5),
            ]
        )
    )
    return table


def build_styles():
    base = getSampleStyleSheet()
    return {
        "Title": ParagraphStyle(
            "Title",
            parent=base["Title"],
            fontName="Helvetica-Bold",
            fontSize=28,
            leading=34,
            textColor=colors.HexColor("#172033"),
            alignment=1,
        ),
        "Subtitle": ParagraphStyle(
            "Subtitle",
            parent=base["Title"],
            fontName="Helvetica",
            fontSize=16,
            leading=21,
            textColor=colors.HexColor("#38445A"),
            alignment=1,
        ),
        "Heading1": ParagraphStyle(
            "Heading1",
            parent=base["Heading1"],
            fontName="Helvetica-Bold",
            fontSize=14,
            leading=18,
            spaceAfter=8,
            textColor=colors.HexColor("#172033"),
        ),
        "Body": ParagraphStyle(
            "Body",
            parent=base["BodyText"],
            fontName="Helvetica",
            fontSize=10.5,
            leading=15,
            spaceAfter=6,
        ),
        "Link": ParagraphStyle(
            "Link",
            parent=base["BodyText"],
            fontName="Helvetica",
            fontSize=11,
            leading=15,
            textColor=colors.HexColor("#0B57D0"),
            spaceAfter=6,
        ),
    }


def main():
    OUTPUT.parent.mkdir(parents=True, exist_ok=True)
    styles = build_styles()
    story = []
    add_title_page(story, styles)
    add_overview(story, styles)
    add_tests(story, styles)

    doc = SimpleDocTemplate(
        str(OUTPUT),
        pagesize=A4,
        rightMargin=2 * cm,
        leftMargin=2 * cm,
        topMargin=1.8 * cm,
        bottomMargin=1.8 * cm,
        title="Testing Report - Word Frequency Analyzer",
    )
    doc.build(story)
    print("Generated " + str(OUTPUT))


if __name__ == "__main__":
    main()
