---
name: tech-documentation
description: "Technical documentation writing instructions."
---

# Technical Documentation Writing Instructions

## 1. Ensure correctness

- Treat documentation quality as an essential part of product quality.
- Verify documented behavior against the implementation, and authoritative references.
- Ensure all examples use supported public interfaces and produce the documented result.
- Never present assumptions, implementation details, or unverified behavior as established fact.
- Prefer accuracy and reader understanding over arbitrary length or completeness targets.

## 2. Write for the intended reader

- Write for readers who are new to the product or library.
- Use language that is clear to readers who may not be native English speakers.
- Introduce each term or concept when the reader first needs it.
- Do not assume knowledge that has not been established earlier in the documentation.
- When linked prerequisite documentation has already established shared concepts, state the prerequisite and focus on what is new instead of re-teaching those concepts.
- Avoid presenting incidental language or implementation terms as new reader-facing concepts when referring directly to the public API is clearer.
- Audit every revision from a new reader’s perspective, looking for logical gaps, unexplained terms, misleading claims, excessive theory, and poor flow.

## 3. Create a coherent learning journey

- Treat the documentation as a continuous reader journey rather than a collection of isolated pages.
- Build on concepts introduced earlier instead of redefining them unnecessarily.
- Present the simplest and most common case first, followed by alternatives and advanced cases.
- Keep related steps, explanations, inputs, and results close together.
- Reuse established scenarios, models, and example flows across related guides when this reduces reader effort; adapt them only where the documented behavior differs.
- End a page with focused next steps when they help readers choose what to learn or configure next.

## 4. Prioritize practical understanding

- Focus on the principles, workflows, and decisions readers must understand.
- Avoid documenting every variation, overload, option, or edge case unless it affects common usage or important decisions.
- Explain abstractions through practical use cases.
- Avoid standalone conceptual sections that do not help readers perform a task or understand behavior.
- Use the simplest interface that correctly demonstrates the behavior.
- When showing a more advanced interface, explain what additional control or capability it provides.
- Describe advanced configuration in terms of its purpose and benefits, not as a failure of simpler approaches.
- Keep documentation depth and page structure proportional to the feature's role and public surface; do not mirror another component's structure mechanically.

## 5. Structure explanations clearly

- Organize procedural explanations in a natural sequence, such as:
    * context or goal
    * required setup or configuration
    * operation or action
    * observable result or behavior
- Give each paragraph one clear purpose.
- Use short, direct sentences where possible.
- Explain shared rules, relationships, and precedence once as general principles instead of repeating local warnings.
- Use tables when readers need to compare related options, mappings, behaviors, or trade-offs.

## 6. Write effective examples

- Make examples realistic, internally consistent, and motivated by a recognizable reader goal.
- Show relevant inputs, requests, commands, outputs, or resulting state when they clarify behavior.
- Keep an example’s setup, action, and result together.
- Do not interrupt an example with unrelated conceptual discussion.
- Do not use a complex technique to reproduce behavior already provided by a simpler technique unless the distinction is important and explicitly explained.
- Cover meaningful alternative forms when readers are likely to encounter them, without expanding into unnecessary implementation detail.

## 7. Edit for usefulness and clarity

- Avoid sentences that only repeat what an example or preceding sentence already makes clear.
- Avoid sentences that merely restate the preceding example or put ‘a hat on a hat.
- Avoid tautological definitions that merely restate a term’s name. Explain what the concept does in the current workflow and why it matters, or omit the definition when its role is already clear.
- Avoid repetition, unnecessary qualifications, and theory that does not affect the reader’s understanding or actions.
- Prefer positive, action-oriented guidance. Use warnings or prohibitions when they prevent a likely mistake or explain a non-obvious consequence.
- Review transitions between paragraphs and sections so that each idea follows naturally from the previous one.
- Make every section meaningful to the reader: begin with the concrete problem, explain why the demonstrated approach is appropriate, and describe its practical effect. Avoid technically accurate but irrelevant implementation details, generic statements, and explanations that merely repeat the example.
